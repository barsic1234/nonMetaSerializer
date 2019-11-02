using System;

namespace nonMetaSerializer.errors
{
    internal enum ErrorCode
    {
        MISMATCH_FIELD_TYPE,
        NOT_SERIALIZABLE,
        UNASSIGNED_PRIMIRIVE
    }
    internal class NonMetaSerializerException : Exception
    {
        public NonMetaSerializerException(ErrorCode errorCode, string nameField) :
            base(GetMessage(errorCode, nameField))
        { }

        private static string GetMessage(ErrorCode errorCode, string nameField)
        {
            switch (errorCode)
            {
                case ErrorCode.MISMATCH_FIELD_TYPE:
                    return "Тип поля " + nameField;
            }
            return "";
        }
    }
}
