using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACK_PTS
{
    class Api
    {
        Database db;
        string path;

        void FillBlock(BinaryReader bin, byte[] array)
        {
            ushort fill = 1;

            using (BinaryWriter bout = new BinaryWriter(bin.BaseStream))
            {
                bout.Write(array.Length);
                bout.Write(fill);
                bout.Write(array, 0, array.Length);
            }
        }

    Api(string path)
        {
            this.path = path;
            db = new Database();
        }

        void New()
        {
            try
            {
                using (BinaryWriter bout = new BinaryWriter(File.OpenWrite(path)))
                {
                    ushort identification = Database.IDENTIFICATION;
                    ushort header = Database.HEADER;
                    bout.Write(identification);
                    bout.Seek(10, SeekOrigin.Begin);
                    bout.Write(header);
                }
            } catch(Exception ex)
            {

            }
        }
        ulong Set(byte[] array)
        {
            ulong id;
            try
            {
                using (BinaryReader bin = new BinaryReader(File.OpenWrite(path)))
                {
                    bin.ReadBytes(Database.HEADER);

                    ushort c_size = 0;
                    ushort fill = 1;
                    id = 0;

                    while (bin.PeekChar() > 0)
                    {
                        c_size = bin.ReadUInt16();
                        fill = bin.ReadUInt16();
                        if (fill == 0)
                        {
                            if (array.Length < c_size)
                            {
                                FillBlock(bin, array);
                                return id;
                            }
                        }
                        ++id;
                    }
                    FillBlock(bin, array);
                }
            }
            catch (Exception ex)
            {

            }
            return id;

        }
        Record Get(ulong id)
        {
            try
            {
                using (BinaryReader bin = new BinaryReader(File.OpenWrite(path)))
                {
                    bin.ReadBytes(Database.HEADER);
                    ushort shift = 0;
                    ulong c_id = 0;
                    ushort size = 0;
                    while (bin.PeekChar() > 0)
                    {
                        if (c_id == id)
                        {
                            size = bin.ReadUInt16();
                            bin.ReadUInt16();
                            byte[] data = bin.ReadBytes(size);
                            Record r = new Record(data);
                            return r;
                        }
                        size = bin.ReadUInt16();
                        bin.ReadBytes(size);
                        ++c_id;
                    }
                }
            } catch (Exception ex) { }
            return null;

        }
        void Update(ulong id, byte[] array)
        {
            try
            {
                using (BinaryReader bin = new BinaryReader(File.OpenWrite(path)))
                {
                    bin.ReadBytes(Database.HEADER);
                    ushort shift = 0;
                    ulong c_id = 0;
                    ushort size = 0;
                    while (bin.PeekChar() > 0)
                    {
                        if (c_id == id)
                        {
                            size = bin.ReadUInt16();
                            if (size < array.Length) return;
                            using (BinaryWriter bout = new BinaryWriter(bin.BaseStream))
                            {
                                bout.Write(array);
                            }
                        }
                        size = bin.ReadUInt16();
                        bin.ReadBytes(size);
                        ++c_id;
                    }
                }
            }
            catch (Exception ex) { }
        }
        void Delete(ulong id)
        {
            try
            {
                using (BinaryReader bin = new BinaryReader(File.OpenWrite(path)))
                {
                    bin.ReadBytes(Database.HEADER);
                    ushort shift = 0;
                    ulong c_id = 0;
                    ushort size = 0;
                    while (bin.PeekChar() > 0)
                    {
                        if (c_id == id)
                        {
                            bin.ReadUInt16();
                            using (BinaryWriter bout = new BinaryWriter(bin.BaseStream))
                            {
                                bout.Write(0);
                            }
                        }
                        size = bin.ReadUInt16();
                        bin.ReadBytes(size);
                        ++c_id;
                    }
                }
            }
            catch (Exception ex) { }
        }
    };

}
