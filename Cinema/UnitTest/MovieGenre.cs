//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnitTest
{
    using System;
    using System.Collections.Generic;
    
    public partial class MovieGenre
    {
        public int IDMovieGenre { get; set; }
        public int IDMovie { get; set; }
        public int IDGenre { get; set; }
    
        public virtual Genre Genre { get; set; }
        public virtual Movie Movie { get; set; }
    }
}