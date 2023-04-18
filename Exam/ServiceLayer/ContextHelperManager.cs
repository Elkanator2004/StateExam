using DataLayer;

namespace ServiceLayer
{
    public class ContextHelperManager
    {
        private static ExamDbContext CurrentContext;
        private static IdentityContext CurrentIdentityContext;
        private static DocumentContext CurrentDocumentContext;

        public static ExamDbContext GenerateDbContext()
        {
            CurrentContext= new ExamDbContext();
            return CurrentContext;
        }

        public static ExamDbContext GetDbContext()
        {
            if (CurrentContext != null)
            {
                return CurrentContext;
            }
            return GenerateDbContext();
        }

        public static IdentityContext GenerateIdentityContext(ExamDbContext dbContext) 
        {
            CurrentIdentityContext = new IdentityContext(dbContext);
            return CurrentIdentityContext;
        }
    }
}