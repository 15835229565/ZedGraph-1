//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright � 2006  John Champion
//
//This library is free software; you can redistribute it and/or
//modify it under the terms of the GNU Lesser General Public
//License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.
//
//This library is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public
//License along with this library; if not, write to the Free Software
//Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//=============================================================================

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ZedGraph
{
	/// <summary>
	/// A class that represents a line segment object on the graph.  A list of
	/// GraphObj objects is maintained by the <see cref="GraphObjList"/> collection class.
	/// </summary>
	/// <remarks>
	/// This should not be confused with the <see cref="LineItem" /> class, which represents
	/// a set of points plotted together as a "curve".  The <see cref="LineObj" /> class is
	/// a single line segment, drawn as a "decoration" on the chart.</remarks>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 1.1.2.4 $ $Date: 2006-04-07 06:14:03 $ </version>
	[Serializable]
	public class LineObj : GraphObj, ICloneable, ISerializable
	{
	#region Fields

		/// <summary>
		/// Private field that stores the color of the arrow.
		/// Use the public property <see cref="Color"/> to access this value.
		/// </summary>
		/// <value>The color value is declared with a <see cref="System.Drawing.Color"/>
		/// specification</value>
		protected Color _color;
		/// <summary>
		/// Private field that stores the width of the pen for drawing the line
		/// segment of the arrow.
		/// Use the public property <see cref="PenWidth"/> to access this value.
		/// </summary>
		/// <value> The width is defined in point units (1/72 inch) </value>
		protected float _penWidth;

		/// <summary>
		/// Private field that stores the <see cref="DashStyle"/> for this
		/// <see cref="LineObj"/>.  Use the public
		/// property <see cref="Style"/> to access this value.
		/// </summary>
		protected DashStyle _style;

	#endregion

	#region Defaults

		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="LineObj"/> class.
		/// </summary>
		new public struct Default
		{
			/// <summary>
			/// The default pen width used for the <see cref="LineObj"/> line segment
			/// (<see cref="LineObj.PenWidth"/> property).  Units are points (1/72 inch).
			/// </summary>
			public static float PenWidth = 1.0F;
			/// <summary>
			/// The default color used for the <see cref="LineObj"/> line segment
			/// and arrowhead (<see cref="LineObj.Color"/> property).
			/// </summary>
			public static Color Color = Color.Red;
			/// <summary>
			/// The default drawing style for the line (<see cref="LineObj.Style"/> property).
			/// This is defined with the <see cref="DashStyle"/> enumeration.
			/// </summary>
			public static DashStyle Style = DashStyle.Solid;
		}

	#endregion

	#region Properties
		/// <summary>
		/// The width of the line segment for the <see cref="LineObj"/>
		/// </summary>
		/// <value> The width is defined in points (1/72 inch) </value>
		/// <seealso cref="Default.PenWidth"/>
		public float PenWidth
		{
			get { return _penWidth; }
			set { _penWidth = value; }
		}
		/// <summary>
		/// The <see cref="System.Drawing.Color"/> of the line segment
		/// </summary>
		/// <value> The color is defined using the
		/// <see cref="System.Drawing.Color"/> class </value>
		/// <seealso cref="Default.Color"/>
		public Color Color
		{
			get { return _color; }
			set { _color = value; }
		}
		/// <summary>
		/// The style of the <see cref="LineObj"/> line, defined as a <see cref="DashStyle"/> enum.
		/// This allows the line to be solid, dashed, or dotted.
		/// </summary>
		/// <seealso cref="Default.Style"/>
		public DashStyle Style
		{
			get { return _style; }
			set { _style = value; }
		}
	#endregion

	#region Constructors

		/// <overloads>Constructors for the <see cref="LineObj"/> object</overloads>
		/// <summary>
		/// A constructor that allows the position, color, and size of the
		/// <see cref="LineObj"/> to be pre-specified.
		/// </summary>
		/// <param name="color">An arbitrary <see cref="System.Drawing.Color"/> specification
		/// for the arrow</param>
		/// <param name="x1">The x position of the starting point that defines the
		/// line.  The units of this position are specified by the
		/// <see cref="Location.CoordinateFrame"/> property.</param>
		/// <param name="y1">The y position of the starting point that defines the
		/// line.  The units of this position are specified by the
		/// <see cref="Location.CoordinateFrame"/> property.</param>
		/// <param name="x2">The x position of the ending point that defines the
		/// line.  The units of this position are specified by the
		/// <see cref="Location.CoordinateFrame"/> property.</param>
		/// <param name="y2">The y position of the ending point that defines the
		/// line.  The units of this position are specified by the
		/// <see cref="Location.CoordinateFrame"/> property.</param>
		public LineObj( Color color, float x1, float y1, float x2, float y2 )
			: base( x1, y1, x2 - x1, y2 - y1 )
		{
			this._penWidth = Default.PenWidth;

			this._color = color;
			this.Location.AlignH = AlignH.Left;
			this.Location.AlignV = AlignV.Top;

			this._style = Default.Style;
		}

		/// <summary>
		/// A constructor that allows only the position of the
		/// line to be pre-specified.  All other properties are set to
		/// default values
		/// </summary>
		/// <param name="x1">The x position of the starting point that defines the
		/// <see cref="LineObj"/>.  The units of this position are specified by the
		/// <see cref="Location.CoordinateFrame"/> property.</param>
		/// <param name="y1">The y position of the starting point that defines the
		/// <see cref="LineObj"/>.  The units of this position are specified by the
		/// <see cref="Location.CoordinateFrame"/> property.</param>
		/// <param name="x2">The x position of the ending point that defines the
		/// <see cref="LineObj"/>.  The units of this position are specified by the
		/// <see cref="Location.CoordinateFrame"/> property.</param>
		/// <param name="y2">The y position of the ending point that defines the
		/// <see cref="LineObj"/>.  The units of this position are specified by the
		/// <see cref="Location.CoordinateFrame"/> property.</param>
		public LineObj( float x1, float y1, float x2, float y2 )
			: this( Default.Color, x1, y1, x2, y2 )
		{
		}

		/// <summary>
		/// Default constructor -- places the <see cref="LineObj"/> at location
		/// (0,0) to (1,1).  All other values are defaulted.
		/// </summary>
		public LineObj() : this( Default.Color, 0, 0, 1, 1 )
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="LineObj"/> object from which to copy</param>
		public LineObj( LineObj rhs ) : base( rhs )
		{
			_color = rhs.Color;
			_penWidth = rhs.PenWidth;
			_style = rhs._style;
		}

		/// <summary>
		/// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
		/// calling the typed version of <see cref="Clone" />
		/// </summary>
		/// <returns>A deep copy of this object</returns>
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>
		/// Typesafe, deep-copy clone method.
		/// </summary>
		/// <returns>A new, independent copy of this class</returns>
		public LineObj Clone()
		{
			return new LineObj( this );
		}

	#endregion

	#region Serialization

		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		// changed to 2 with addition of Style property
		public const int schema2 = 10;

		/// <summary>
		/// Constructor for deserializing objects
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data
		/// </param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data
		/// </param>
		protected LineObj( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema2" );

			_color = (Color)info.GetValue( "color", typeof( Color ) );
			_penWidth = info.GetSingle( "penWidth" );
			_style = (DashStyle)info.GetValue( "style", typeof( DashStyle ) );
		}
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data</param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data</param>
		[SecurityPermissionAttribute( SecurityAction.Demand, SerializationFormatter = true )]
		public override void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			base.GetObjectData( info, context );

			info.AddValue( "schema2", schema2 );

			info.AddValue( "color", _color );
			info.AddValue( "penWidth", _penWidth );
			info.AddValue( "style", _style );
		}
	#endregion

	#region Rendering Methods
		/// <summary>
		/// Render this object to the specified <see cref="Graphics"/> device.
		/// </summary>
		/// <remarks>
		/// This method is normally only called by the Draw method
		/// of the parent <see cref="GraphObjList"/> collection object.
		/// </remarks>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="PaneBase"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		override public void Draw( Graphics g, PaneBase pane, float scaleFactor )
		{
			// Convert the arrow coordinates from the user coordinate system
			// to the screen coordinate system
			PointF pix1 = this.Location.TransformTopLeft( pane );
			PointF pix2 = this.Location.TransformBottomRight( pane );

			if ( pix1.X > -10000 && pix1.X < 100000 && pix1.Y > -100000 && pix1.Y < 100000 &&
				pix2.X > -10000 && pix2.X < 100000 && pix2.Y > -100000 && pix2.Y < 100000 )
			{
				// calculate the length and the angle of the arrow "vector"
				double dy = pix2.Y - pix1.Y;
				double dx = pix2.X - pix1.X;
				float angle = (float)Math.Atan2( dy, dx ) * 180.0F / (float)Math.PI;
				float length = (float)Math.Sqrt( dx * dx + dy * dy );

				// Save the old transform matrix
				Matrix transform = g.Transform;
				// Move the coordinate system so it is located at the starting point
				// of this arrow
				g.TranslateTransform( pix1.X, pix1.Y );
				// Rotate the coordinate system according to the angle of this arrow
				// about the starting point
				g.RotateTransform( angle );

				// get a pen according to this arrow properties
				Pen pen = new Pen( this._color, pane.ScaledPenWidth( _penWidth, scaleFactor ) );
				pen.DashStyle = this._style;

				g.DrawLine( pen, 0, 0, length, 0 );

				// Restore the transform matrix back to its original state
				g.Transform = transform;
			}
		}

		/// <summary>
		/// Determine if the specified screen point lies inside the bounding box of this
		/// <see cref="LineObj"/>.
		/// </summary>
		/// <remarks>The bounding box is calculated assuming a distance
		/// of <see cref="GraphPane.Default.NearestTol"/> pixels around the arrow segment.
		/// </remarks>
		/// <param name="pt">The screen point, in pixels</param>
		/// <param name="pane">
		/// A reference to the <see cref="PaneBase"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>true if the point lies in the bounding box, false otherwise</returns>
		override public bool PointInBox( PointF pt, PaneBase pane, Graphics g, float scaleFactor )
		{
			// transform the x,y location from the user-defined
			// coordinate frame to the screen pixel location
			PointF pix = this._location.TransformTopLeft( pane );
			PointF pix2 = this._location.TransformBottomRight( pane );

			Pen pen = new Pen( Color.Black, (float)GraphPane.Default.NearestTol * 2.0F );
			GraphicsPath path = new GraphicsPath();
			path.AddLine( pix, pix2 );
			return path.IsOutlineVisible( pt, pen );
		}

	#endregion

	}
}
