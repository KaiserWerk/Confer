using Core.Model;

namespace Core
{
    public class TransmitRequestedFileMessage
    {
        public RequestedFile Content { get; set; }

        public TransmitRequestedFileMessage(RequestedFile content)
        {
            this.Content = content;
        }
    }
}
