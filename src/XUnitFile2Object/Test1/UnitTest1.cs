using System;
using System.Collections.Generic;
using System.Data;
using Coder.File2Object;
using Coder.File2Object.Columns;
using Xunit;

namespace XUnitFile2Object.Test1
{
    public class StudentAchievement
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Achievement { get; set; }

        public DateTime AchievementCreateTime { get; set; }
    }

    public class StudentAchievementImportManager : Excel2ObjectManager<StudentAchievement>
    {
        protected override StudentAchievement Create()
        {
            return new StudentAchievement();
        }
    }

    public class UnitTest1
    {
        [Fact]
        public void ReadExcel()
        {
            string fielName = "test1.xlsx";
            StudentAchievementImportManager manager = new StudentAchievementImportManager();
            manager.Titles = new List<string>()
            {
                "编码",   "名称",   "成绩",   "注册时间"

            };
            manager.Column(f => f.Code);
            manager.Column(f => f.Name);
            manager.Column(f => f.Achievement);
            manager.Column(f => f.AchievementCreateTime);
            manager.TryRead(fielName, out var datas, out var resultFile);

            Assert.Equal(7, datas.Count);
        }

        [Fact]
        public void ReadExcelEmpty()
        {
            string fielName = "test_empty.xlsx";
            StudentAchievementImportManager manager = new StudentAchievementImportManager();
            manager.Titles = new List<string>()
            {
                "编码",   "名称",   "成绩",   "注册时间"

            };
            manager.Column(f => f.Code);
            manager.Column(f => f.Name);
            manager.Column(f => f.Achievement);
            manager.Column(f => f.AchievementCreateTime);
            manager.TryRead(fielName, out var datas, out var resultFile);

            Assert.Equal(0, datas.Count);
        }


        [Fact]
        public void ReadExcel_DataTypeNotMatch()
        {
            string fielName = "test_dataType_not_match.xlsx";
            StudentAchievementImportManager manager = new StudentAchievementImportManager();
            manager.Titles = new List<string>()
            {
                "编码",   "名称",   "成绩",   "注册时间"

            };
            manager.Column(f => f.Code);
            manager.Column(f => f.Name);
            manager.Column(f => f.Achievement);
            manager.Column(f => f.AchievementCreateTime);
            manager.TryRead(fielName, out var datas, out var resultFile);


            Assert.Equal(3, datas[0].CellErrors.Count);
            Assert.True(datas[0].HasError);
        }

        [Fact]
        public void ReadExcel_String_Require()
        {

            string fielName = "test1_require_string.xlsx";
            StudentAchievementImportManager manager = new StudentAchievementImportManager();
            manager.Titles = new List<string>()
            {
                "编码",   "名称",   "成绩",   "注册时间"

            };
            manager.Column(f => f.Code);
            manager.Column(f => f.Name, true);
            manager.Column(f => f.Achievement);
            manager.Column(f => f.AchievementCreateTime);
            manager.TryRead(fielName, out var datas, out var resultFile);


            Assert.Equal(1, datas[0].CellErrors.Count);
            Assert.True(datas[0].HasError);
        }
    }
}

