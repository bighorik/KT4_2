namespace Application.Utils
{
    public static class ErrorCodes
    {
        public static string UserNotFound => nameof(UserNotFound);
        public static string EmailAlreadyTaken => nameof(EmailAlreadyTaken);
        public static string AccountConfirmationCodeAlreadySent => nameof(AccountConfirmationCodeAlreadySent);
        public static string ActualAccountConfirmationCodeNotFound => nameof(ActualAccountConfirmationCodeNotFound);
        public static string ProjectNotFound => nameof(ProjectNotFound);
        public static string ProjectMemberNotFound => nameof(ProjectMemberNotFound);
        public static string ProjectPositionNotFound => nameof(ProjectPositionNotFound);
        public static string ProjectAlreadyArchived => nameof(ProjectAlreadyArchived);
        public static string AttachmentNotFound => nameof(AttachmentNotFound);
        public static string StudentGroupNotFound => nameof(StudentGroupNotFound);
        public static string RequiredSkillNotFound => nameof(RequiredSkillNotFound);
        public static string SkillNotFound => nameof(SkillNotFound);
        public static string UserIsNotProjectOwner => nameof(UserIsNotProjectOwner);
        public static string MembershipApplicationNotFound => nameof(MembershipApplicationNotFound);
        public static string UserIsNotOwnerMembershipApplication => nameof(UserIsNotOwnerMembershipApplication);
    }
}
