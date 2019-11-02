namespace nonMetaSerializer.researchAlgorithm
{
    internal delegate byte[] StreamExtractorHandler(int streamLength);
    interface IPrimitive
    {
        byte[] GetByteStream(object valueField);
        object GetValueField(StreamExtractorHandler streamExtractor);
    }
}
