
var GenericReactGridSubDetailView = React.createClass({


    renderRows(gridRows, numberOfColumns) {
        var self = this;
        if (!gridRows || gridRows.length === 0) {
            return (null);
        }

        return gridRows.map(function (gridRow, index) {
            return (
                <tr key={index}>
                    <td>{gridRow.col1}</td>
                    {numberOfColumns >= 2 &&
                        <td>{gridRow.col2}</td>
                    }
                    {numberOfColumns >= 3 &&
                        <td>{gridRow.col3}</td>
                    }
                    {numberOfColumns >= 4 &&
                        <td>{gridRow.col4}</td>
                    }
                    {numberOfColumns >= 5 &&
                        <td>{gridRow.col5}</td>
                    }

                </tr>
            );
        }, self);
    },

    renderHeaders(gridHeaders) {
        var self = this;
        return gridHeaders.map(function (item, index) {
            var header = '';
            if (self.props.isTopSubGrid) {
                header = item.Header;
            }
            if (item.Width && item.Width.length > 0) {
                return (
                    <th key={index}
                        style={{ width: item.Width }}>  {header}</th>
                ); 
            }
            return (
                <th key={index}> {header}</th>
            );
        }, self);
    },

    render() {

        if (this.props.data === null) {
            return (null);
        }
        var currentGrid = this.props.data;
        var headers = this.renderHeaders(currentGrid.GridHeader);
        var rowData = this.renderRows(currentGrid.GridRow, currentGrid.NumberOfColumns);


        if (this.props.isTopSubGrid) {
            return (
				<table className="generic-sub-grid" >
                    <thead >
                        <tr>
                            {headers}
                        </tr>
                    </thead>
                    <tbody>
                        {rowData}
                    </tbody>
                </table >

            );
        }

        return (
            <table className="generic-sub-grid secondary">
                <thead>
                    <tr>
                        {headers}
                    </tr>
                </thead>
                <tbody>
                    {rowData}
                </tbody>
            </table >

        );
    }
});