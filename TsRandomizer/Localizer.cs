﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using Timespinner.Core.Localization;
using TsRandomizer.Extensions;
using TsRandomizer.IntermediateObjects;

namespace TsRandomizer
{
	class Localizer : DynamicObject
	{
		static readonly Type Type = TimeSpinnerType
			.Get("Timespinner.Core.Localization.Loc");

		public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
		{
			try
			{
				result = Type
					 .GetMethod(binder.Name, BindingFlags.Static | BindingFlags.NonPublic)
					 .Invoke(null, args);

				return true;
			}
			catch
			{
				result = null;
				return false;
			}
		}

		public void OverrideKey(string key, string value)
		{
			try
			{
				var stringLibrary = (StringLibrary) Type
					.GetField("_currentLibrary", BindingFlags.Static | BindingFlags.NonPublic)
					.GetValue(null);

				var stringInstances = (Dictionary<string, StringInstance>) stringLibrary.AsDynamic()._stringInstances;

				stringInstances[key].Text = value;
			}
			catch
			{
			}
		}
	}
}
