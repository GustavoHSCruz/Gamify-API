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

        [Description("Password and confirm password must be equal")]
        PasswordAndConfirmPasswordMustBeEqual,

        #endregion

        #region D

        #endregion

        #region E

        [Description("Email already in use")] EmailAlreadyInUse,

        [Description("Email pattern not match")]
        EmailPatternNotMatch,

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
        [Description("Must be business days")]
        MustBeBusinessDays,
        
        [Description("Must be weekend" )]
        MustBeWeekend,
        #endregion

        #region N

        [Description("Quest not found")] QuestNotFound,

        #endregion

        #region O
        
        [Description("Only one week day can be selected")]
        OnlyOneWeekDay,
        #endregion

        #region P

        [Description("Password pattern not match")]
        PasswordPatternNotMatch,

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

        [Description("Wrong email or password")]
        WrongEmailOrPassword,

        #endregion

        #region X

        #endregion

        #region Y

        #endregion

        #region Z

        #endregion
    }
}