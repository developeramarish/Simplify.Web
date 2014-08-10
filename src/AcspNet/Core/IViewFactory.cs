﻿using System;
using AcspNet.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Represent view factory
	/// </summary>
	public interface IViewFactory
	{
		/// <summary>
		/// Creates the view.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="viewType">Type of the view.</param>
		/// <returns></returns>
		IView CreateView(IDIContainerProvider containerProvider, Type viewType);
	}
}