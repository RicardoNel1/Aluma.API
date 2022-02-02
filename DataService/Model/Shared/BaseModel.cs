using System;

namespace DataService.Model
{
    // all other models must inherit from this model
    public class BaseModel
    {
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public int ModifiedBy { get; set; }

        public BaseModel()
        {
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }
    }
}