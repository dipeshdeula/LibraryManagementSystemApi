using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BooksEntity>> GetAllBooksAsync()
        {
           return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<BooksEntity> GetBooksByIdAsync(int id)
        {
            return await _bookRepository.GetBooksByIdAsync(id);
        }


        public async Task<BooksEntity> CreateBooksAsync(BooksEntity books)
        {
           await _bookRepository.AddBooksAsync(books);
            return books;
        }


        public async Task UpdateBooksAsync(BooksEntity books)
        {
            await _bookRepository.UpdateBooksAsync(books);
        }

        public async Task DeleteBooksAsync(int id)
        {
           await _bookRepository.DeleteBooksAsync(id);
        }

    }
}
