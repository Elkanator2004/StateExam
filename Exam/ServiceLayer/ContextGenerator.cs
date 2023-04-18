using DataLayer;

namespace ServiceLayer
{
    public class ContextGenerator
    {
        private static ExamDbContext examcontext;
        private static DocumentContext documentcontext;

        public static ExamDbContext CreateDbContext()
        {
            examcontext = new ExamDbContext();
            return examcontext;
        }

        public static ExamDbContext GetDbContext()
        {
            return examcontext;
        }
        public static DocumentContext CreateDocumentContext(ExamDbContext dbContext)
        {
            documentcontext = new DocumentContext(dbContext);
            return documentcontext;
        }
        public static DocumentContext GetDocumentContext()
        {
            return documentcontext;
        }
    }
}