namespace AdoExtend
{
    using System.Data;

    public class WYDataSet : DataSet
    {

        public override void Load(IDataReader _IDataReader, LoadOption _LoadOption, FillErrorEventHandler _FillErrorEventHandler, params DataTable[] Tables)
        {
            WYDataAdapter _WYDataAdapter = new WYDataAdapter
            {
                FillLoadOption = _LoadOption,
                MissingSchemaAction = MissingSchemaAction.AddWithKey
            };
            if (_FillErrorEventHandler != null) _WYDataAdapter.FillError += _FillErrorEventHandler;
            _WYDataAdapter.FillFromReader(this, _IDataReader, 0, 0);
            if (!_IDataReader.IsClosed && !_IDataReader.NextResult()) _IDataReader.Close();
        }


    }
}
