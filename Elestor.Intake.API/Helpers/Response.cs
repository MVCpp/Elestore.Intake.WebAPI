using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Elestor.Intake.API.Helpers
{
    public class Response<T>
    {
        private T _data;
        private bool _success = true;
        private string _message;

        [DataMember()]
        public bool success
        {
            get { return _success; }
            set { _success = value; }
        }

        [DataMember()]
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }

        [DataMember()]
        public T data
        {
            get { return _data; }
            set { _data = value; }
        }

        public Response(ref T data)
        {
            _data = data;
        }
    }
}