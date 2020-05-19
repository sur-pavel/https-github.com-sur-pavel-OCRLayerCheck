using ManagedClient;
using System;
using System.Collections;

namespace OCRLayerCheck
{
    internal class IrbisHandler
    {
        internal bool connected = false;
        private ManagedClient64 client = new ManagedClient64();
        private IrbisRecord recordFrom = new IrbisRecord();
        private IrbisRecord recordTo = new IrbisRecord();
        private string _recordIndex;

        internal void Connect(string database, string login, string password)
        {
            try
            {
                if (connected)
                {
                    Disconnect();
                }
                client.ParseConnectionString("host=127.0.0.1;port=8888; user=a;password=1;");
                //client.ParseConnectionString("host=194.169.10.3;port=8888; user=" + login + ";password=" + password + ";");
                client.Connect();
                client.PushDatabase(database);
                connected = true;
                Console.WriteLine("Connect");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        internal string Rename(string name)
        {
            return name;
        }

        internal void Disconnect()
        {
            try
            {
                client.Disconnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        internal void CopyRecord(string fromMfn, string toMfn, string recordIndex, string recordType)
        {
            try
            {
                recordFrom = client.ReadRecord(Int32.Parse(fromMfn));
                recordTo = client.ReadRecord(Int32.Parse(toMfn));
                if (recordType.Equals("дублеты"))
                {
                    EditDoublets();
                }
                if (recordType.Equals("рукописи"))
                {
                    EditManuscripts(recordIndex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void EditManuscripts(string recordIndex)
        {
            _recordIndex = "(" + recordIndex + ") ";
            RecordField fiedl922 = new RecordField("922");
            AddSubfield(fiedl922, 'C', "200", 'A');
            AddSubfield(fiedl922, 'F', "700", 'A');
            AddSubfield(fiedl922, '?', "700", 'G');
            AddSubfield(fiedl922, '1', "700", '1');
            AddSubfield(fiedl922, 'A', "700", 'C');
            recordTo.Fields.Add(fiedl922);

            CopyFieldWithIndex("300");
            CopyFieldWithIndex("331");
            CopySubField("215", 'A');
            CopySubField("316", 'A');

            foreach (RecordField field210 in recordFrom.Fields.GetField("210"))
            {
                if (!field210.ToString().Contains("^5"))
                {
                    _recordIndex = _recordIndex.Replace(" ", "");
                }

                var field = new RecordField("210");
                var sub2105 = new SubField('5', _recordIndex + field210.GetFirstSubFieldText('5'));
                var sub210d = new SubField('D', field210.GetFirstSubFieldText('d'));
                field.SubFields.Add(sub210d);
                field.SubFields.Add(sub2105);
                Console.WriteLine(field.Text);
                recordTo.Fields.Add(field);
                Console.WriteLine(recordTo.FM("210"));
                client.WriteRecord
                            (
                                recordTo,
                                false,
                                true
                            );
            }
        }

        private void EditDoublets()
        {
            // writing to RecordTo
            CopyField("686");
            CopyField("606");
            CopyField("830");
            CopyField("910");
            CopyField("317");
            CopyField("316");
            client.WriteRecord
                (
                recordTo,
                false,
                true
                );

            // writing to RecordFrom
            recordFrom.AddField("910", 'A', "0", 'B', "000-6");
            recordFrom.AddField("503", 'A', "ЗАПОЛНИТЬ НОВОЙ ЗАПИСЬЮ");
            client.WriteRecord
                (
                recordFrom,
                false,
                true
                );
        }

        private void CopyFieldWithIndex(string tagFrom)
        {
            if (!String.IsNullOrEmpty(recordFrom.FM(tagFrom)))
            {
                foreach (string field in recordFrom.FMA(tagFrom))
                {
                    recordTo.Fields.Add(new RecordField(tagFrom, _recordIndex + field));
                    Console.WriteLine(tagFrom + ": " + field);
                }
            }
        }

        private void CopyField(string tagFrom)
        {
            if (recordFrom.HaveField(tagFrom))
            {
                ArrayList list = new ArrayList();
                foreach (RecordField field in recordTo.Fields.GetField(tagFrom))
                {
                    list.Add(field.ToSortedText());
                }
                foreach (RecordField field in recordFrom.Fields.GetField(tagFrom))
                {
                    if (!list.Contains(field.ToSortedText()))
                    {
                        recordTo.Fields.Add(field);
                    }
                }
            }
        }

        private void CopyField(string tagFrom, string tagTo)
        {
            if (!String.IsNullOrEmpty(recordFrom.FM(tagFrom)))
            {
                foreach (string field in recordFrom.FMA(tagFrom))
                {
                    recordTo.Fields.Add(new RecordField(tagTo, _recordIndex + field));
                }
            }
        }

        private void CopySubField(string tagFrom, char codeFrom)
        {
            if (!String.IsNullOrEmpty(recordFrom.FM(tagFrom, codeFrom)))
            {
                foreach (string field in recordFrom.FMA(tagFrom, codeFrom))
                {
                    recordTo.AddField(tagFrom, codeFrom, _recordIndex + field);
                }
            }
        }

        private void CopySubField(string tagFrom, char codeFrom, string tagTo, char codeTo)
        {
            if (!String.IsNullOrEmpty(recordFrom.FM(tagFrom, codeFrom)))
            {
                foreach (string field in recordFrom.FMA(tagFrom, codeFrom))
                {
                    recordTo.AddField(tagTo, codeTo, _recordIndex + field);
                }
            }
        }

        private void AddSubfield(RecordField field, char subFieldCode, string tagFrom, char codeFrom)
        {
            if (!String.IsNullOrEmpty(recordFrom.FM(tagFrom, codeFrom)))
            {
                SubField subField = new SubField(subFieldCode, recordFrom.FM(tagFrom, codeFrom));
                field.SubFields.Add(subField);
            }
        }
    }
}