using AbiokaRdn.ApplicationService.Validation;
using AbiokaRdn.Domain.Repositories;
using AbiokaRdn.Infrastructure.Common.Exceptions;
using AbiokaRdn.Infrastructure.Common.Helper;
using FluentValidation;
using System;
using System.IO;

namespace AbiokaRdn.ApplicationService.Messaging
{
    public class FilePathRequest : ServiceRequestBase
    {
        public string Path { get; set; }
    }

    public class FilePathRequestValidator : CustomValidator<FilePathRequest>
    {
        private readonly IExceptionLogRepository exceptionLogRepository;

        public FilePathRequestValidator(IExceptionLogRepository exceptionLogRepository) {
            this.exceptionLogRepository = exceptionLogRepository;

            RuleFor(r => r.Path).NotEmpty().WithMessage("IsRequired");
        }

        protected override void DataValidate(FilePathRequest instance, ActionType actionType) {
            if (File.Exists(instance.Path))
                return;

            exceptionLogRepository.Add(new ExceptionLog(
                "FilePathRequestValidator", string.Empty, "FileNotFound", "FileNotFound",
                $"File does not exits. Path: {instance.Path}", Guid.Empty, "0.0.0.0"));

            throw new FileNotFoundException("FileNotFound", instance.Path);
        }
    }
}
