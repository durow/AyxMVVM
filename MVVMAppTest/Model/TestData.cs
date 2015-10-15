using AyxMVVM;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MVVMAppTest.Model
{
    public class TestData:ObserveObject
    {
        private bool _BoolValue;

        public bool BoolValue
        {
            get { return _BoolValue; }
            set
            {
                SetAndNotifyIfChanged("BoolValue", ref _BoolValue, value);
            }
        }

        private DateTime _AddDateTime;

        public DateTime AddDateTime
        {
            get { return _AddDateTime; }
            set
            {
                SetAndNotifyIfChanged("AddDateTime", ref _AddDateTime, value);
            }
        }

        private string _Remark;

        public string Remark
        {
            get { return _Remark; }
            set
            {
                SetAndNotifyIfChanged("Remark", ref _Remark, value);
            }
        }

        private string _Img = "Assets/StoreLogo.png";

        public string Img
        {
            get { return _Img; }
            set
            {
                SetAndNotifyIfChanged("Img", ref _Img, value);
            }
        }

        public static IEnumerable<TestData> InitData()
        {
            for (int i = 0; i < 5; i++)
            {
                yield return GetInstance();
            }
        }

        public static TestData GetInstance()
        {
            return new TestData
            {
                AddDateTime = DateTime.Now,
                Remark = "Remark Test String!",
                BoolValue = true
            };
        }
    }
}
