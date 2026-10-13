namespace Application.Utils
{
    public static class ErrorCodes
    {
        public static string UserNotFound => nameof(UserNotFound);
        public static string EmailNotConfirmed => nameof(EmailNotConfirmed);
        public static string PasswordNotCorrect => nameof(PasswordNotCorrect);
        public static string EmailAlreadyTaken => nameof(EmailAlreadyTaken);
        public static string AccountConfirmationCodeAlreadySent => nameof(AccountConfirmationCodeAlreadySent);
        public static string ActualAccountConfirmationCodeNotFound => nameof(ActualAccountConfirmationCodeNotFound);
        public static string ProjectNotFound => nameof(ProjectNotFound);
        public static string InternetResourceLinkNotFound => nameof(InternetResourceLinkNotFound);
        public static string UserContactNotFound => nameof(UserContactNotFound);
        public static string ProjectMemberNotFound => nameof(ProjectMemberNotFound);
        public static string ProjectPositionNotFound => nameof(ProjectPositionNotFound);
        public static string ProjectPositionSkillNotFound => nameof(ProjectPositionSkillNotFound);
        public static string UserSkillNotFound => nameof(UserSkillNotFound);
        public static string ProjectAlreadyArchived => nameof(ProjectAlreadyArchived);
        public static string AttachmentNotFound => nameof(AttachmentNotFound);
        public static string StudentGroupNotFound => nameof(StudentGroupNotFound);
        public static string RequiredSkillNotFound => nameof(RequiredSkillNotFound);
        public static string SkillNotFound => nameof(SkillNotFound);
        public static string NoModeAvailableSkills => nameof(NoModeAvailableSkills);
        public static string SkillAlreadyAdded => nameof(SkillAlreadyAdded);
        public static string StudentGroupNumberNotFound => nameof(StudentGroupNumberNotFound);
        public static string UserIsNotProjectOwner => nameof(UserIsNotProjectOwner);
        public static string MembershipApplicationNotFound => nameof(MembershipApplicationNotFound);
        public static string UserIsNotOwnerMembershipApplication => nameof(UserIsNotOwnerMembershipApplication);
        public static string InvalidToken => nameof(InvalidToken);
        public static string InvalidRefreshToken => nameof(InvalidRefreshToken);
    }
}
