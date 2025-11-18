using AutoMapper;
using Domain.Shared.Entities;
using Domain.Shared.Enums;
using Domain.Shared.Events;
using Domain.Shared.Interfaces;
using Domain.Shared.Requests;
using Domain.Shared.Responses;
using Domain.Shared.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Common
{
    public abstract class Command<TEntity, TRequest, TResponse, TEvent> : ServiceHandler<TRequest, TResponse>
        where TEntity : Entity, IAggregateRoot
        where TRequest : Request<TResponse>
        where TResponse : CommandResponse
        where TEvent : Event
    {
        protected readonly IMediator _mediator;
        private TEntity _entity;
        protected IMapper _mapper;
        protected IWriteRepository _repository;
        protected readonly IUnitOfWork _uow;

        protected Command(IMediator mediator, IWriteRepository repository, IUnitOfWork uow, IMapper mapper)
        {
            _response = Activator.CreateInstance<TResponse>();
            _mediator = mediator;
            _repository = repository;
            _uow = uow;
            _mapper = mapper;
        }

        protected abstract Task BeforeChanges(TRequest request);

        protected abstract Task<TEntity> Changes(TRequest request);

        protected virtual Task AfterChanges(TEntity entity)
        {
            _response.Id = entity.Id;

            return Task.FromResult(_response);
        }

        protected virtual TEvent? CreateEvent(TEntity entity)
        {
            return default;
        }

        public override async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await BeforeChanges(request);

                if (_response.Errors.Any())
                    return _response;

                _entity = await Changes(request);

                if (_response.Errors.Any())
                    return _response;

                await _uow.SaveChangesAsync();
            }
            catch (OperationCanceledException)
            {
                _response.StatusCode = StatusCodes.Status499ClientClosedRequest;
                _response.InternalMessage = "Operação cancelada pelo cliente.";
                return _response;
            }
            catch (Exception ex)
            {
                // TODO: Adicionar logging aqui
                // _logger.LogError(ex, "Erro durante execução do comando {CommandType} para request {RequestType}", 
                //     GetType().Name, typeof(TRequest).Name);

                _response.StatusCode = StatusCodes.Status500InternalServerError;
                _response.InternalMessage = "Erro na execução do comando.";

                return _response;
            }

            // Segunda fase: Processamento pós-transação (não crítico)
            try
            {
                await AfterChanges(_entity);

                var @event = CreateEvent(_entity);
                if (@event != null)
                {
                    @event.SetIpAddress(request.GetIpAddr());

                    await _mediator.Publish(@event, cancellationToken);
                }
                
                return _response;
            }
            catch (Exception ex)
            {
                // TODO: Adicionar logging aqui
                // _logger.LogWarning(ex, "Erro no processamento pós-transação para comando {CommandType}, mas dados foram salvos", 
                //     GetType().Name);

                _response.StatusCode = StatusCodes.Status206PartialContent;
                _response.InternalMessage = "Alguns conteúdos foram salvos, mas ocorreu um erro.";

                return _response;
            }
        }
    }
}