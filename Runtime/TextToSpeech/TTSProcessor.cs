/*
This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software Foundation,
Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.

The Original Code is Copyright (C) 2020 Voxell Technologies.
All rights reserved.
*/

using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Voxell.Speech.TTS
{
  public partial class TextToSpeech
  {
    public TextAsset mapper;

    internal struct Mapper
    {
      public Dictionary<string, int> symbol_to_id;
      public Dictionary<int, string> id_to_symbol;
    }

    private Mapper _mapper;

    private void InitTTSProcessor()
    {
      _mapper = JsonConvert.DeserializeObject<Mapper>(mapper.text);
    }

    /// <summary>
    /// Perform preprocessing and raw feature extraction for LJSpeech dataset
    /// </summary>
    /// <param name="text">input text</param>
    /// <returns>array of integers translated letter by letter</returns>
    private int[] TextToSequence(string text)
    {
      List<int> sequence = new List<int>();

      foreach (char l in text)
      {
        if (_ShouldKeepSymbol(l))
        {
          try { sequence.Add(_mapper.symbol_to_id[l.ToString()]); }
          catch { Debug.LogWarning($"Symbol not in dictionary: {l}"); }
        }
      }

      sequence.Add(_mapper.symbol_to_id["eos"]);
      return sequence.ToArray();
    }

    private static bool _ShouldKeepSymbol(char symbol) =>
      symbol != '_' &&
      symbol != '~' &&
      Convert.ToUInt16(symbol) != 8203;
  }
}