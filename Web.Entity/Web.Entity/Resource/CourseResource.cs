using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Resource
{
    public class PagingParam<Entity>
    {
        public string SortExpression { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Skip { get; set; }
        public int NotSkip { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int CategoryId { get; set; }

        public virtual List<Expression<Func<Entity, bool>>> GetPredicates()
        {
            return new List<Expression<Func<Entity, bool>>>();
        }
        
    }
    public class CourseResource
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Status { get; set; }
    }

    public class CourseFillterResource : PagingParam<CourseEntity>
    {
        public int Status { get; set; }
        public string KeyWord { get; set; }

        public override List<Expression<Func<CourseEntity, bool>>> GetPredicates()
        {
            var filter = base.GetPredicates();
            if (!string.IsNullOrEmpty(KeyWord))
            {
                KeyWord = KeyWord.ToLower();
                filter.Add(c => c.Title.ToLower().Contains(KeyWord));
                
            }

            if (MinPrice > 0 && MaxPrice > 0)
            {
                filter.Add(c => c.Price < MaxPrice && c.Price > MinPrice);
            }
            
            if (Status > 0)
            {
                filter.Add(x => x.Status == Status);
            }
            return filter;
        }
    }
}
