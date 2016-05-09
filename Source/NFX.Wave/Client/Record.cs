﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NFX.DataAccess.CRUD;
using NFX.Serialization.JSON;

namespace NFX.Wave.Client
{
  /// <summary>
  /// Represents a client-side "Form" an analogue of WAVE.RecordModel.Record on C# server-side.
  /// The class is initialized from schema obtained from the server via JSON.
  /// The JSON is generated by RecordModelGenerator class.
  /// This class is usually used for API consumers that need to have the JS/client-like functionality on the server
  /// </summary>
  public class Record : DynamicRow
  {

    public Record(string init) : base()
    {
      if (init.IsNullOrWhiteSpace()) throw new WaveException(StringConsts.ARGUMENT_ERROR+"Record.ctor(init==null|empty)");
      
      JSONDataMap initMap = null;
      try
      {
         initMap = JSONReader.DeserializeDataObject(init) as JSONDataMap;
      }
      catch(Exception error)
      {
        throw new WaveException(StringConsts.ARGUMENT_ERROR+"Record.ctor(init is bad): "+error.ToMessageWithType(), error);
      }

      if (initMap ==null) throw new WaveException(StringConsts.ARGUMENT_ERROR+"Record.ctor(init isnot map)");

      ctor(initMap);
    }

    public Record(JSONDataMap init) : base()
    {
      if (init==null) throw new WaveException(StringConsts.ARGUMENT_ERROR+"Record.ctor(init(map)==null)");

      ctor(init);
    }

    private void ctor(JSONDataMap init)
    {
      m_Init = init;
      var schema = MapInitToSchema();
      __ctor(schema);
      LoadData();
    }


    protected JSONDataMap m_Init;


    public JSONDataMap Init{ get{ return m_Init;}}



    protected virtual Schema MapInitToSchema()
    {
      var defs = new List<Schema.FieldDef>();

      //loop{
      var attr = new FieldAttribute("aaaa");
      var ftp = typeof(string);
      var def = new Schema.FieldDef("First_Name", ftp, attr); 

      defs.Add(def);
      //}loop

      return new Schema("aaaa", false, defs);
    }

    protected virtual void LoadData()
    {
      //loop
      this["First_Name"] = "object";
    }


  }




}
