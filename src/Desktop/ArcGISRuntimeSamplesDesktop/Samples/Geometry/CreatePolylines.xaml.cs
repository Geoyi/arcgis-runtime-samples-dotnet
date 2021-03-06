﻿using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ArcGISRuntime.Samples.Desktop
{
	/// <summary>
	/// Demonstrates how to create polyline geometries, attach them to graphics and display them on the map. Polyline geometry objects are used to store geographic lines.
	/// </summary>
	/// <title>Create Polylines</title>
	/// <category>Geometry</category>
	public partial class CreatePolylines : UserControl
	{
		private GraphicsOverlay _graphicsOverlay;

		/// <summary>Construct Create Polylines sample control</summary>
		public CreatePolylines()
		{
			InitializeComponent();

			_graphicsOverlay = MyMapView.GraphicsOverlays["graphicsOverlay"];
			MyMapView.NavigationCompleted += MyMapView_NavigationCompleted;
		}

		// Create polyline graphics on the map in the center and the center of four equal quadrants
		private void MyMapView_NavigationCompleted(object sender, EventArgs e)
		{
			MyMapView.NavigationCompleted -= MyMapView_NavigationCompleted;
			try
			{
				// Get current viewpoints extent from the MapView
				var currentViewpoint = MyMapView.GetCurrentViewpoint(ViewpointType.BoundingGeometry);
				var viewpointExtent = currentViewpoint.TargetGeometry.Extent;
				var myViewpointExtent = viewpointExtent;
				var height = myViewpointExtent.Height / 4;
				var width = myViewpointExtent.Width / 4;
				var length = width / 4;
				var center = myViewpointExtent.GetCenter();

				var topLeft = new MapPoint(center.X - width, center.Y + height, MyMapView.SpatialReference);
				var topRight = new MapPoint(center.X + width, center.Y + height, MyMapView.SpatialReference);
				var bottomLeft = new MapPoint(center.X - width, center.Y - height, MyMapView.SpatialReference);
				var bottomRight = new MapPoint(center.X + width, center.Y - height, MyMapView.SpatialReference);

				var redSymbol = new SimpleLineSymbol()
				{
					Color = System.Windows.Media.Colors.Red,
					Width = 4,
					Style = SimpleLineStyle.Solid
				};
				var blueSymbol = new SimpleLineSymbol()
				{
					Color = System.Windows.Media.Colors.Blue,
					Width = 4,
					Style = SimpleLineStyle.Solid
				};

				_graphicsOverlay.Graphics.Add(new Graphic() { Geometry = CreatePolylineX(center, length), Symbol = blueSymbol });
				_graphicsOverlay.Graphics.Add(new Graphic() { Geometry = CreatePolylineX(topLeft, length), Symbol = redSymbol });
				_graphicsOverlay.Graphics.Add(new Graphic() { Geometry = CreatePolylineX(topRight, length), Symbol = redSymbol });
				_graphicsOverlay.Graphics.Add(new Graphic() { Geometry = CreatePolylineX(bottomLeft, length), Symbol = redSymbol });
				_graphicsOverlay.Graphics.Add(new Graphic() { Geometry = CreatePolylineX(bottomRight, length), Symbol = redSymbol });
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error occurred : " + ex.Message, "Create Polygons Sample");
			}
		}

		// Creates a polyline with two paths in the shape of an 'X' centered at the given point
		private Polyline CreatePolylineX(MapPoint center, double length)
		{
			var halfLen = length / 2.0;

			LineSegment segment = new LineSegment(
				new MapPoint(center.X - halfLen, center.Y + halfLen, MyMapView.SpatialReference),
				new MapPoint(center.X + halfLen, center.Y - halfLen, MyMapView.SpatialReference));

			LineSegment segment2 = new LineSegment(
				new MapPoint(center.X + halfLen, center.Y + halfLen, MyMapView.SpatialReference),
				new MapPoint(center.X - halfLen, center.Y - halfLen, MyMapView.SpatialReference));

			var segmentCollection = new SegmentCollection(MyMapView.SpatialReference)
			{
				segment
			};

			var segmentCollection2 = new SegmentCollection(MyMapView.SpatialReference)
			{
				segment2
			};

			return new Polyline(new [] { segmentCollection, segmentCollection2},
				MyMapView.SpatialReference);
		}
	}
}
