
using integral_api.Models;
using Microsoft.EntityFrameworkCore;

namespace integral_api.Data

{
    public class QnAContext : DbContext
    {
        public QnAContext(DbContextOptions<QnAContext> options) : base(options)
        {
        }

        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<AnswerModel> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionModel>().HasData(
                new QuestionModel { Id = 5, Text = "Что такое компонент в React?" },
                new QuestionModel { Id = 6, Text = "Что такое JSX в React?" },
                new QuestionModel { Id = 7, Text = "Как создать состояние (state) в React?" }
            );

            modelBuilder.Entity<AnswerModel>().HasData(
                new AnswerModel { Id = 10, QuestionId = 5, Text = "Это функция, которая возвращает React-элемент", IsCorrect = true },
                new AnswerModel { Id = 11, QuestionId = 5, Text = "Это HTML-тег в React", IsCorrect = false },
                new AnswerModel { Id = 12, QuestionId = 5, Text = "Это класс в JavaScript", IsCorrect = false },

                new AnswerModel { Id = 13, QuestionId = 6, Text = "Это расширение языка JavaScript", IsCorrect = true },
                new AnswerModel { Id = 14, QuestionId = 6, Text = "Это язык разметки, похожий на HTML", IsCorrect = false },
                new AnswerModel { Id = 15, QuestionId = 6, Text = "Это встроенный шаблонизатор", IsCorrect = false },

                new AnswerModel { Id = 16, QuestionId = 7, Text = "Используя функцию setState", IsCorrect = true },
                new AnswerModel { Id = 17, QuestionId = 7, Text = "Создавая переменную с именем state", IsCorrect = false },
                new AnswerModel { Id = 18, QuestionId = 7, Text = "React не поддерживает состояние", IsCorrect = false }
            );
        }
    }

}