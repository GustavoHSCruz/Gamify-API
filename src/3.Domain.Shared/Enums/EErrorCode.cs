using System.ComponentModel;

namespace Domain.Shared.Enums
{
    public enum EErrorCode : byte
    {
        #region A

        #endregion

        #region B

        #endregion

        #region C

        [Description("Cannot update refresh token")]
        CannotUpdateRefreshToken,

        #endregion

        #region D

        #endregion

        #region E

        [Description("Email already in use")] EmailAlreadyInUse,

        [Description("Email or password wrong")]
        EmailOrPasswordWrong,

        #endregion

        #region F

        #endregion

        #region G

        #endregion

        #region H

        #endregion

        #region I

        #endregion

        #region J

        #endregion

        #region K

        #endregion

        #region L

        #endregion

        #region M

        #endregion

        #region N
        [Description("No quest found")]
        NoQuestFound,
        #endregion

        #region O

        #endregion

        #region P

        #endregion

        #region Q

        [Description("Quest activitie already scheduled")]
        QuestActivitieAlreadyScheduled,

        #endregion

        #region R

        #endregion

        #region S

        #endregion

        #region T

        #endregion

        #region U

        [Description("User not found")] UserNotFound,

        #endregion

        #region V

        #endregion

        #region W

        #endregion

        #region X

        #endregion

        #region Y

        #endregion

        #region Z

        #endregion
    }
}