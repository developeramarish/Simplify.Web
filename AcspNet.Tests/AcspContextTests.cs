﻿using System.Web.Routing;
using AcspNet.TestingHelper;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class AcspContextTests
	{
		[Test]
		public void Constructor_CorrectValues_PropertiesInitializedCorrectly()
		{
			var httpContext = AcspTestingHelper.CreateTestHttpContext();
			var context = new AcspContext(httpContext.Object);

			Assert.IsNotNull(context.HttpContext);
			Assert.IsNotNull(context.Request);
			Assert.IsNotNull(context.Response);
			Assert.IsNotNull(context.Form);
			Assert.IsNotNull(context.QueryString);
			Assert.IsNotNull(context.Session);
			Assert.AreEqual("C:/WebSites/TestSite", context.SitePhysicalPath);
			Assert.AreEqual("/TestSite", context.SiteVirtualPath);
			Assert.AreEqual("http://localhost/TestSite/", context.SiteUrl);
		}

		[Test]
		public void Constructor_CorrectValuesFromQueryString_ActionAndModeInitializedCorrectly()
		{
			var httpContext = AcspTestingHelper.CreateTestHttpContext();

			httpContext.Object.Request.QueryString.Add("act", "Foo");
			httpContext.Object.Request.QueryString.Add("mode", "Bar");
			httpContext.Object.Request.QueryString.Add("id", "5");

			var context = new AcspContext(httpContext.Object);

			Assert.AreEqual("Foo", context.CurrentAction);
			Assert.AreEqual("Bar", context.CurrentMode);
			Assert.AreEqual("5", context.CurrentID);
		}

		[Test]
		public void Constructor_CorrectValuesFromQueryString_ActionModeIdUrlCalculatedCorrectly()
		{
			var httpContext = AcspTestingHelper.CreateTestHttpContext();

			httpContext.Object.Request.QueryString.Add("act", "Foo");
			httpContext.Object.Request.QueryString.Add("mode", "Bar");
			httpContext.Object.Request.QueryString.Add("id", "5");

			var context = new AcspContext(httpContext.Object);

			Assert.AreEqual("?act=Foo&amp;mode=Bar&amp;id=5", context.GetActionModeUrl());
		}

		[Test]
		public void Constructor_CorrectValuesFromQueryString_ActionIdUrlCalculatedCorrectly()
		{
			var httpContext = AcspTestingHelper.CreateTestHttpContext();

			httpContext.Object.Request.QueryString.Add("act", "Foo");
			httpContext.Object.Request.QueryString.Add("id", "5");

			var context = new AcspContext(httpContext.Object);

			Assert.AreEqual("?act=Foo&amp;id=5", context.GetActionModeUrl());
		}

		[Test]
		public void Constructor_ActionIsNull_EmptyStringReturned()
		{
			var httpContext = AcspTestingHelper.CreateTestHttpContext();

			var context = new AcspContext(httpContext.Object);

			Assert.AreEqual("", context.GetActionModeUrl());
		}

		[Test]
		public void Constructor_NewSession_IsNewSessionIsTrue()
		{
			var httpContext = AcspTestingHelper.CreateTestHttpContext();

			var context = new AcspContext(httpContext.Object);

			Assert.IsTrue(context.IsNewSession);
		}

		[Test]
		public void Constructor_ExistingSession_IsNewSessionIsFalse()
		{
			var httpContext = AcspTestingHelper.CreateTestHttpContext();

			httpContext.Setup(x => x.Session[It.Is<string>(c => c == AcspContext.IsNewSessionFieldName)]).Returns("false");

			var context = new AcspContext(httpContext.Object);

			Assert.IsFalse(context.IsNewSession);
		}
	}
}