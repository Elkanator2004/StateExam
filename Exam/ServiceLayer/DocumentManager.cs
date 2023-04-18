using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class DocumentManager
    {
        private readonly DocumentContext documentContext;

        public DocumentManager(DocumentContext documentContext)
        {
            this.documentContext = documentContext;
        }

        public async Task CreateAsync(Document document)
        {
            try
            {
                await documentContext.CreateAsync(document);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Document> ReadAsync(string entrynumber, bool useNavigationalProperties = false)
        {
            try
            {
                return await documentContext.ReadAsync(entrynumber, useNavigationalProperties);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Document>> ReadAllAsync(bool useNavigationalProperties = false)
        {
            try
            {
                return await documentContext.ReadAllAsync(useNavigationalProperties);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(Document document, bool useNavigationalProperties = false)
        {
            try
            {
                await documentContext.UpdateAsync(document, useNavigationalProperties);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteAsync(string entrynumber)
        {
            try
            {
                await documentContext.DeleteAsync(entrynumber);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
