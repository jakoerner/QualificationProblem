using NUnit.Framework;
using QualificationProblem;

namespace QualificationProblemTests
{
    [TestFixture]
    public class AggregatedUnitTests
    {
        #region Setup/TearDown
        private Grid _defaultGrid = null;

        [OneTimeSetUp]
        public void DoSetUp()
        {
            _defaultGrid = new Grid();
        }

        [OneTimeTearDown]
        public void DoTearDown()
        {
            _defaultGrid = null;
        }
        #endregion

        #region VertexExtensions Tests
        [Test]
        public void VertexExtensions_IsCoincident_CoincidentVertices_ReturnsTrue()
        {
            var first = new Vertex() { X = 10, Y = 10 };
            var second = new Vertex() { X = 10, Y = 10 };
            Assert.IsTrue(first.IsCoincident(second));
        }

        [Test]
        public void VertexExtensions_IsCoincident_NonCoincidentVertices_ReturnsFalse()
        {
            var first = new Vertex() { X = 10, Y = 10 };
            var second = new Vertex() { X = 20, Y = 10 };
            Assert.IsFalse(first.IsCoincident(second));
        }
        #endregion

        #region TriangleExtensions Tests
        [Test]
        public void TriangleExtensions_IsCoincident_CoincidentVertices_SameWindOrderAndOrigin_ReturnsTrue()
        {
            var vertex0 = new Vertex { X = 10, Y = 10 };
            var vertex1 = new Vertex { X = 20, Y = 10 };
            var vertex2 = new Vertex { X = 20, Y = 20 };

            var expectedTriangle = new Triangle { Vertices = new Vertex[] { vertex0, vertex1, vertex2 } };
            var testTriangle = new Triangle { Vertices = new Vertex[] { vertex0, vertex1, vertex2 } };

            Assert.IsTrue(expectedTriangle.IsCoincident(testTriangle));
        }

        [Test]
        public void TriangleExtensions_IsCoincident_CoincidentVertices_DifferentWindOrderSameOrigin_ReturnsTrue()
        {
            var vertex0 = new Vertex { X = 10, Y = 10 };
            var vertex1 = new Vertex { X = 20, Y = 10 };
            var vertex2 = new Vertex { X = 20, Y = 20 };

            var expectedTriangle = new Triangle { Vertices = new Vertex[] { vertex0, vertex1, vertex2 } };
            var testTriangle = new Triangle { Vertices = new Vertex[] { vertex0, vertex2, vertex1 } };

            Assert.IsTrue(expectedTriangle.IsCoincident(testTriangle));
        }
        [Test]
        public void TriangleExtensions_IsCoincident_CoincidentVertices_SameWindOrderDifferentOrigin_ReturnsTrue()
        {
            var vertex0 = new Vertex { X = 10, Y = 10 };
            var vertex1 = new Vertex { X = 20, Y = 10 };
            var vertex2 = new Vertex { X = 20, Y = 20 };

            var expectedTriangle = new Triangle { Vertices = new Vertex[] { vertex0, vertex1, vertex2 } };
            var testTriangle = new Triangle { Vertices = new Vertex[] { vertex1, vertex2, vertex0 } };

            Assert.IsTrue(expectedTriangle.IsCoincident(testTriangle));
        }
        [Test]
        public void TriangleExtensions_IsCoincident_NonCoincidentVertices_ReturnsFalse()
        {
            var vertex0 = new Vertex { X = 10, Y = 10 };
            var vertex1 = new Vertex { X = 20, Y = 10 };
            var vertex2 = new Vertex { X = 20, Y = 20 };
            var vertex2a = new Vertex { X = 30, Y = 20 };

            var expectedTriangle = new Triangle { Vertices = new Vertex[] { vertex0, vertex1, vertex2 } };
            var testTriangle = new Triangle { Vertices = new Vertex[] { vertex0, vertex1, vertex2a } };

            Assert.IsFalse(expectedTriangle.IsCoincident(testTriangle));
        }

        [Test]
        public void TriangleExtensions_WithPosition_EvenColumnId_ReturnsExpectedValue()
        {
            //test with A2 coordinates:
            var cellOrigin = new Vertex() { X = 0, Y = 0 };
            var vertex0 = new Vertex { X = 10, Y = 0 };
            var vertex1 = new Vertex { X = 10, Y = 10 };
            var vertex2 = new Vertex { X = 0, Y = 0 };

            var expectedTriangle = new Triangle { Vertices = new Vertex[] { vertex0, vertex1, vertex2 } };
            var actualTriangle = new Triangle().WithPosition(cellOrigin, _defaultGrid, true);

            Assert.IsTrue(expectedTriangle.IsCoincident(actualTriangle));
        }

        [Test]
        public void Triangle_WithPosition_OddColumnId_ReturnsExpectedValue()
        {
            //test with A1 coordinates:
            var cellOrigin = new Vertex() { X = 0, Y = 0 };
            var vertex0 = new Vertex { X = 0, Y = 10 };
            var vertex1 = new Vertex { X = 0, Y = 0 };
            var vertex2 = new Vertex { X = 10, Y = 10 };

            var expectedTriangle = new Triangle { Vertices = new Vertex[] { vertex0, vertex1, vertex2 } };
            var actualTriangle = new Triangle().WithPosition(cellOrigin, _defaultGrid, false);

            Assert.IsTrue(expectedTriangle.IsCoincident(actualTriangle));
        }
        #endregion

        #region MiscExtensions Tests
        [Test]
        public void MiscExtensions_IsEven_EvenInput_ReturnsTrue()
        {
            Assert.IsTrue(2.IsEven());
        }
        [Test]
        public void MiscExtensions_IsEven_OddInput_ReturnsFalse()
        {
            Assert.IsFalse(3.IsEven());
        }
        #endregion

        #region GridExtensions Tests
        [Test]
        public void GridExtensions_GetRowIndex_DefaultGrid_ReturnsExpectedValue()
        {
            Assert.AreEqual(1, _defaultGrid.GetRowIndex('B'));
        }
        [Test]
        public void GridExtensions_GetRowIndex_DefaultGrid_ValueOutOfRange_ReturnsExpectedValue()
        {
            Assert.AreEqual(-1, _defaultGrid.GetRowIndex('Z'));
        }

        [Test]
        public void GridExtensions_GetColumnIndex_DefaultGrid_EvenIdValue_ReturnsExpectedValue()
        {
            Assert.AreEqual(1, _defaultGrid.GetColumnIndex(4));
        }
        [Test]
        public void GridExtensions_GetColumnIndex_DefaultGrid_OddIdValue_ReturnsExpectedValue()
        {
            Assert.AreEqual(3, _defaultGrid.GetColumnIndex(7));
        }
        [Test]
        public void GridExtensions_GetColumnIndex_DefaultGrid_LowValueOutOfRange_ReturnsExpectedValue()
        {
            Assert.AreEqual(-1, _defaultGrid.GetColumnIndex(_defaultGrid.MinColumn - 1));
        }
        [Test]
        public void GridExtensions_GetColumnIndex_DefaultGrid_HighValueOutOfRange_ReturnsExpectedValue()
        {
            Assert.AreEqual(-1, _defaultGrid.GetColumnIndex(_defaultGrid.MaxColumn + 1));
        }
        
        [Test]
        public void GridExtensions_GetGridCellOrigin_DefaultGrid_OutOfRangeColumnIndex_ReturnsNull()
        {
            Assert.IsNull(_defaultGrid.GetGridCellOrigin('B', _defaultGrid.MaxColumn + 1));
        }

        [Test]
        public void GridExtensions_GetGridCellOrigin_DefaultGrid_EvenColumnIndex_ReturnsExpectedValue()
        {
            var expected = new Vertex() { X = 10, Y = 10 };
            var actual = (Vertex)_defaultGrid.GetGridCellOrigin('B', 4);

            //check coordinates individually to see the point of failure, if any:
            Assert.AreEqual(expected.X, actual.X, "X");
            Assert.AreEqual(expected.Y, actual.Y, "Y");

            //final pass/fail (only passes if both of the above pass...)
            Assert.IsTrue(expected.IsCoincident(actual));
        }
        [Test]
        public void GridExtensions_GetGridCellOrigin_DefaultGrid_OddColumnIndex_ReturnsExpectedValue()
        {
            var expected = new Vertex() { X = 10, Y = 10 };
            var actual = (Vertex)_defaultGrid.GetGridCellOrigin('B', 3);

            //check coordinates individually to see the point of failure, if any:
            Assert.AreEqual(expected.X, actual.X, "X");
            Assert.AreEqual(expected.Y, actual.Y, "Y");

            //final pass/fail (only passes if both of the above pass...)
            Assert.IsTrue(expected.IsCoincident(actual));
        }

        [Test]
        public void GridExtensions_MaxX_DefaultGrid_ReturnsExpectedValue()
        {
            Assert.AreEqual(60, _defaultGrid.MaxX());
        }

        [Test]
        public void GridExtensions_MaxY_DefaultGrid_ReturnsExpectedValue()
        {
            Assert.AreEqual(60, _defaultGrid.MaxY());
        }

        [Test]
        public void GridExtensions_IsValidRowId_ValidValues_ReturnsTrue()
        {
            Assert.IsTrue(_defaultGrid.IsValidRowId(_defaultGrid.MinRow),"MinValue");
            Assert.IsTrue(_defaultGrid.IsValidRowId(_defaultGrid.MaxRow), "MaxValue");
            Assert.IsTrue(_defaultGrid.IsValidRowId((char)(((int)_defaultGrid.MinRow+(int)_defaultGrid.MaxRow)/2)), "MidValue");
        }

        [Test]
        public void GridExtensions_IsValidRowId_InvalidValue_ReturnsFalse()
        {
            Assert.IsFalse(_defaultGrid.IsValidRowId((char)((int)_defaultGrid.MinRow-1)), "LowValue");
            Assert.IsFalse(_defaultGrid.IsValidRowId((char)((int)_defaultGrid.MaxRow + 1)), "HighValue");
        }

        [Test]
        public void GridExtensions_IsValidColumnId_ValidValues_ReturnsTrue()
        {
            Assert.IsTrue(_defaultGrid.IsValidColumnId(_defaultGrid.MinColumn), "MinValue");
            Assert.IsTrue(_defaultGrid.IsValidColumnId(_defaultGrid.MaxColumn), "MaxValue");
            Assert.IsTrue(_defaultGrid.IsValidColumnId((_defaultGrid.MinColumn + _defaultGrid.MaxColumn) / 2), "MidValue");
        }

        [Test]
        public void GridExtensions_IsValidColumnId_InvalidValue_ReturnsFalse()
        {
            Assert.IsFalse(_defaultGrid.IsValidColumnId(_defaultGrid.MinColumn - 1), "LowValue");
            Assert.IsFalse(_defaultGrid.IsValidColumnId(_defaultGrid.MaxColumn + 1), "HighValue");
        }
        #endregion

        #region First Problem Tests:  Triangle Coordinate Calculations (GridExtensions)
        [Test]
        public void GridExtensions_GetTriangleByRowColumn_DefaultGrid_A1_ReturnsExpectedValue()
        {
            var vertex0 = new Vertex { X = 0, Y = 10 };
            var vertex1 = new Vertex { X = 0, Y = 0 };
            var vertex2 = new Vertex { X = 10, Y = 10 };

            var expectedTriangle = new Triangle { Vertices = new Vertex[] { vertex0, vertex1, vertex2 } };
            var actualTriangle = _defaultGrid.GetTriangleByRowColumn('A', 1);

            Assert.IsNotNull(actualTriangle);
            Assert.IsTrue(expectedTriangle.IsCoincident(actualTriangle.Value));
        }

        [Test]
        public void GridExtensions_GetTriangleByRowColumn_DefaultGrid_A2_ReturnsExpectedValue()
        {
            var vertex0 = new Vertex { X = 10, Y = 0 };
            var vertex1 = new Vertex { X = 10, Y = 10 };
            var vertex2 = new Vertex { X = 0, Y = 0 };

            var expectedTriangle = new Triangle { Vertices = new Vertex[] { vertex0, vertex1, vertex2 } };
            var actualTriangle = _defaultGrid.GetTriangleByRowColumn('A', 2);

            Assert.IsNotNull(actualTriangle);
            Assert.IsTrue(expectedTriangle.IsCoincident(actualTriangle.Value));
        }

        [Test]
        public void GridExtensions_GetTriangleByRowColumn_DefaultGrid_F11_ReturnsExpectedValue()
        {
            var vertex0 = new Vertex { X = 50, Y = 60 };
            var vertex1 = new Vertex { X = 50, Y = 50 };
            var vertex2 = new Vertex { X = 60, Y = 60 };

            var expectedTriangle = new Triangle { Vertices = new Vertex[] { vertex0, vertex1, vertex2 } };
            var actualTriangle = _defaultGrid.GetTriangleByRowColumn('F', 11);

            Assert.IsNotNull(actualTriangle);
            Assert.IsTrue(expectedTriangle.IsCoincident(actualTriangle.Value));
        }

        [Test]
        public void GridExtensions_GetTriangleByRowColumn_DefaultGrid_F12_ReturnsExpectedValue()
        {
            var vertex0 = new Vertex { X = 60, Y = 50 };
            var vertex1 = new Vertex { X = 60, Y = 60 };
            var vertex2 = new Vertex { X = 50, Y = 50 };

            var expectedTriangle = new Triangle { Vertices = new Vertex[] { vertex0, vertex1, vertex2 } };
            var actualTriangle = _defaultGrid.GetTriangleByRowColumn('F', 12);

            Assert.IsNotNull(actualTriangle);
            Assert.IsTrue(expectedTriangle.IsCoincident(actualTriangle.Value));
        }

        [Test]
        public void GridExtensions_GetTriangleByRowColumn_DefaultGrid_C7_ReturnsExpectedValue()
        {
            var vertex0 = new Vertex { X = 30, Y = 30 };
            var vertex1 = new Vertex { X = 30, Y = 20 };
            var vertex2 = new Vertex { X = 40, Y = 30 };

            var expectedTriangle = new Triangle { Vertices = new Vertex[] { vertex0, vertex1, vertex2 } };
            var actualTriangle = _defaultGrid.GetTriangleByRowColumn('C', 7);

            Assert.IsNotNull(actualTriangle);
            Assert.IsTrue(expectedTriangle.IsCoincident(actualTriangle.Value));
        }

        [Test]
        public void GridExtensions_GetTriangleByRowColumn_DefaultGrid_D6_ReturnsExpectedValue()
        {
            var vertex0 = new Vertex { X = 30, Y = 30 };
            var vertex1 = new Vertex { X = 30, Y = 40 };
            var vertex2 = new Vertex { X = 20, Y = 30 };

            var expectedTriangle = new Triangle { Vertices = new Vertex[] { vertex0, vertex1, vertex2 } };
            var actualTriangle = _defaultGrid.GetTriangleByRowColumn('D', 6);

            Assert.IsNotNull(actualTriangle);
            Assert.IsTrue(expectedTriangle.IsCoincident(actualTriangle.Value));
        }
        #endregion

        #region Second Problem Tests:  Calculate Triangle RowId/ColumnId from set of 3 vertices (GridExtensions)
        [Test]
        public void GridExtensions_GetTriangleRowColumnFromVertices_DefaultGrid_C7_ReturnsExpectedValue()
        {
            var vertex0 = new Vertex { X = 30, Y = 30 };
            var vertex1 = new Vertex { X = 30, Y = 20 };
            var vertex2 = new Vertex { X = 40, Y = 30 };

            var verts = new Vertex[] { vertex0, vertex1, vertex2 };

            char rowId = '?';
            int columnId = -1;

            //calculate row/column id and see if result is found:
            Assert.IsTrue(_defaultGrid.GetTriangleRowColumnFromVertices(verts, out rowId, out columnId), "Check result found");
            //now check returned values:
            Assert.AreEqual('C', rowId, "RowId");
            Assert.AreEqual(7, columnId, "ColumnId");
        }

        [Test]
        public void GridExtensions_GetTriangleRowColumnFromVertices_DefaultGrid_D6_ReturnsExpectedValue()
        {
            var vertex0 = new Vertex { X = 30, Y = 30 };
            var vertex1 = new Vertex { X = 30, Y = 40 };
            var vertex2 = new Vertex { X = 20, Y = 30 };

            var verts = new Vertex[] { vertex0, vertex1, vertex2 };

            char rowId = '?';
            int columnId = -1;

            //calculate row/column id and see if result is found:
            Assert.IsTrue(_defaultGrid.GetTriangleRowColumnFromVertices(verts, out rowId, out columnId), "Check result found");
            //now check returned values:
            Assert.AreEqual('D', rowId, "RowId");
            Assert.AreEqual(6, columnId, "ColumnId");
        }

        //we could continue to test for failure conditions, etc...
       
        #endregion
    }
}
