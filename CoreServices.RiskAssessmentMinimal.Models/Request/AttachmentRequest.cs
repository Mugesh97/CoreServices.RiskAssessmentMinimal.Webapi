using Edrs.ActionListenerService.Models.Common;
using Edrs.ActionListenerService.Models.DTO;
using Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Converters;
using Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types;
using Edrs.ActionListenerService.Models.Exceptions;
using Edrs.ActionListenerService.Models.Helpers;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators;
using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Edrs.ActionListenerService.Models.Request
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(EDRSAttachmentDtoValidator))]
    public class EDRSAttachmentDto : CommonRequestModel
    {
        public EDRSAttachmentDto()
        {
            this.InvalidDtoException = new InvalidEDRSAttachmentParametersException();
        }

        [DataMember(IsRequired = true)]
        [RestrictedCharacterField(Constants.ApplicationMessageIdRestrictionRegex)]
        public string ApplicationMessageId { get; set; } // Case ID
        [DataMember(IsRequired = true)]
        [RestrictedCharacterField(Constants.ExternalReferenceRestrictionRegex)]
        public string ExternalReference { get; set; } // Attachment Reference.
        [DataValidation()]
        [JsonConverter(typeof(ItemConverter))]
        [DataMember(IsRequired = true)]
        public Item Item { get; set; }
        public string CallbackUrl { get; set; }
        [JsonProperty(PropertyName = "event")]
        public LogEventsDto LogEventsDto { get; set; }
        [RestrictedCharacterField(Constants.AdditionalProviderFilterRestrictionRegex)]
        public string AdditionalProviderFilter { get; set; } = string.Empty;

        public override DtoException GetDtoException()
        {
            this.PerformDtoSpecificValidation();
            return base.GetDtoException();
        }

        public void PerformDtoSpecificValidation()
        {
            //Check item type
            Item = Item ?? throw new ArgumentNullException(nameof(Item));

            if (this.Item.ItemType.ToString() == "Attachment")
            {
                ValidateAttachment();
            }
            else if (this.Item.ItemType.ToString() == "Note")
            {
                ValidateNote();
            }
        }
        public void ValidateAttachment()
        {
            //Access attachment properties
            var attachment = (Attachment)Item;

            //Check that content isn't empty
            if (string.IsNullOrWhiteSpace(attachment.Content.Data))
            {
                var constraint = "cannot be null";

                this.InvalidDtoException.DtoValidationMessage = $" - data is not valid with respect to validation constraint: {constraint}";
                throw this.InvalidDtoException;
            }

            //Validate file size doesn't exceed 40MB
            var byteArray = Convert.FromBase64String(attachment.Content.Data);
            if (byteArray.Length > FileFormatHelper.EDRSMaxAttachmentFileSize)
            {
                var constraint = "file size exceeds 40MB";

                this.InvalidDtoException.DtoValidationMessage = $" - data is not valid with respect to validation constraint: {constraint}";
                throw this.InvalidDtoException;
            }

            //Validate file content
            byte[] fileByteArray = Encoding.UTF8.GetBytes(attachment.Content.Data);
            MemoryStream content = new MemoryStream(byteArray);
            var contentFileFormat = FileFormatHelper.GetContentFileFormat(content);
            if (!FileFormatHelper.allowedFileTypes.Exists(fileType => string.Equals(fileType, contentFileFormat.ToLower())))
            {
                var constraint = $"must contain file type from one of: {string.Join(",", FileFormatHelper.allowedFileTypes)}";

                this.InvalidDtoException.DtoValidationMessage = $" - 'content.data' value is not valid with respect to validation constraint: {constraint}";
                throw this.InvalidDtoException;
            }

            //Validate filename, only if it is there.
            if (!string.IsNullOrWhiteSpace(attachment.Content.Name))
            {
                var fileNameFileFormat = FileFormatHelper.GetExtension(attachment.Content.Name);

                if (!FileFormatHelper.allowedFileTypes.Exists(fileType => string.Equals(fileType, fileNameFileFormat.ToLower())))
                {
                    var constraint = $"must be <your file name>.<extension> where extension is one of: {string.Join(",", FileFormatHelper.allowedFileTypes)}";

                    this.InvalidDtoException.DtoValidationMessage = $" - 'name' value is not valid with respect to validation constraint: {constraint}";
                    throw this.InvalidDtoException;
                }

                //Check the file type matches that of the encoded attachment data type apart from jpeg/jpg
                if (!String.Equals(contentFileFormat, fileNameFileFormat, StringComparison.OrdinalIgnoreCase))
                {
                    var constraint = "the attachment file format must match the specified file format";

                    this.InvalidDtoException.DtoValidationMessage = $" - data is not valid with respect to validation constraint: {constraint}";
                    throw this.InvalidDtoException;
                }
            }
        }

        public void ValidateNote()
        {
            //Access note properties
            var note = (Note)Item;

            //Verify that notes isn't blank
            if (string.IsNullOrWhiteSpace(note.Notes))
            {
                var constraint = "cannot be null";
                this.InvalidDtoException.DtoValidationMessage = $" - notes is not valid with respect to validation constraint: {constraint}";
                throw this.InvalidDtoException;
            }
        }
    }
}
