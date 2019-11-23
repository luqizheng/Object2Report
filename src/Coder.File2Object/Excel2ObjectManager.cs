using Coder.File2Object.Readers;
using NPOI.SS.UserModel;

namespace Coder.File2Object
{
    public abstract class Excel2ObjectManager<TEntity> : File2ObjectManager<TEntity, ICell>
    {
        protected Excel2ObjectManager(int sheetIndex = 0) : base(new ExcelFileReader(sheetIndex))
        {
        }
    }
}