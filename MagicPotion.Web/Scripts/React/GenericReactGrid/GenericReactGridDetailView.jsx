
var GenericReactGridDetailView = React.createClass({
    getInitialState: function () {
        return {
            showLoadingBox: false,
            data: null,
            StatusPostData: this.props.StatusPostData
        }
    },
    componentWillMount: function () {
        var self = this;

        $(this.props.refreshSelector).on('click',
            function (event) {
                self.refresh();
            });

        self.refresh();
    },

    refresh() {
        var self = this;
        var url = this.props.dataUrl;

        $.ajax({
            type: "GET",
            url: url
        }).done(function (data, status, xhr) {

            var newData = JSON.parse(JSON.stringify(data));
            var isPreviousNullOrHasNoRows = self.isDataNullOrHasNoRows(self.state.data);
            var isCurrentNullOrHasNoRows = self.isDataNullOrHasNoRows(newData);

            self.setState({ data: newData });
            self.checkStatusForUpdate(isCurrentNullOrHasNoRows, isPreviousNullOrHasNoRows);
        }, self)
            .fail(function () {
                return null;
            });
    },

    isDataNullOrHasNoRows(data) {
        if (data &&
            data.Grid[0] &&
            data.Grid[0].GridRow.length &&
            data.Grid[0].GridRow.length > 0) {
            return false;
        }
        return true;
    },

    checkStatusForUpdate(isCurrentNullOrHasNoRows, isPreviousNullOrHasNoRows) {
        var currentlyHasGreenCheck = this.state.StatusPostData.hasData;

        //Main logic
        /* We only care about two main tranistion:
        When we have a green check we only need to watch for if all rows get deleted.
        When we don't we only have to watch for when a row is created.

        */
        if (currentlyHasGreenCheck === true) {
            if (!isPreviousNullOrHasNoRows && isCurrentNullOrHasNoRows) {
                this.updateStatus(false);
            }
        } else {
            if (isPreviousNullOrHasNoRows && !isCurrentNullOrHasNoRows) {
                this.updateStatus(true);
            }
        }

        //if prev is null and current has rows and not has check -> add it

        //if prev has rows and current is null and has check -> remove it;

    },

    updateStatus(sectionHasData) {
        var self = this;
        this.state.StatusPostData.hasData = sectionHasData;

        $.ajax({
            type: "POST",
            url: self.props.StatusPostUrl,
            data: JSON.stringify(self.state.StatusPostData),
            contentType: "application/json; charset=utf-8",
            traditional: true
        }).done(function (data, status, xhr) {
                
            console.log('updated  status to ' + sectionHasData);
                self.props.addRemoveCheckMark(self.state.StatusPostData.dataDetailId, sectionHasData);
            }, self)
            .fail(function () {
                return null;
            });
    },

    renderStringColumn(columnValue) {
        if (!columnValue) {
            return (<td></td>);
        }
        var substring = "<br";
        if (columnValue.indexOf(substring) !== -1) {
            return (<td dangerouslySetInnerHTML={{ __html: columnValue }} />);
        }

        return (<td>{columnValue}</td>);
    },

    renderRows(gridRows, numberOfColumns, dataId) {
        var self = this;
        if (!gridRows || gridRows.length === 0) {
            return (<tr>
                <td colSpan={numberOfColumns + 1}><strong>No data was returned.</strong></td>
            </tr>);
        }

        return gridRows.map(function (gridRow, index) {
            var hiddenInputs = [];
            if (gridRow.deleteParams) {
                if (gridRow.deleteParams.caseId) {
                    hiddenInputs.push(<input type="hidden" id={"delete_caseId_" + gridRow.id} value={gridRow.deleteParams.caseId} />);
                }
                if (gridRow.deleteParams.type) {
                    hiddenInputs.push(<input type="hidden" id={"delete_type_" + gridRow.id} value={gridRow.deleteParams.type} />);
                }
            }

            return (
                <tr key={index}>
                    {self.renderStringColumn(gridRow.col1)}

                    {(numberOfColumns >= 2 && typeof gridRow.col2 === 'string') &&
                        self.renderStringColumn(gridRow.col2)
                    }
                    {(numberOfColumns >= 2 && typeof gridRow.col2 !== 'string') &&
                        <td style={{ padding: '0 1px 0 0' }} ><GenericReactGridSubDetailView data={gridRow.col2} isTopSubGrid={index === 0} /></td>
                    }


                    {(numberOfColumns >= 3 && typeof gridRow.col3 === 'string') &&
                        self.renderStringColumn(gridRow.col3)
                    }
                    {(numberOfColumns >= 3 && typeof gridRow.col3 !== 'string') &&
                        <td style={{ padding: '0 1px 0 0' }} ><GenericReactGridSubDetailView data={gridRow.col3} isTopSubGrid={index === 0} /></td>
                    }


                    {numberOfColumns >= 4 &&
                        self.renderStringColumn(gridRow.col4)
                    }


                    {numberOfColumns >= 5 &&
                        self.renderStringColumn(gridRow.col5)
                    }
                    <td style={{ width: '90px' }} >
                        <a className="pull-right btn-sm" title="Delete" style={{ background: 'transparent' }} data-action="delete" data-action-id={gridRow.id} data-delete-url={gridRow.deleteUrl}>
                            <i className="fa fa-trash"></i>
                        </a>
                        <a className="pull-right btn-sm" style={{ background: 'transparent' }} title="Edit" data-pe-dialog={gridRow.updateUrl} data-close-dialog-function="loadGrid" data-close-dialog-function-params={dataId + ",true"}>
                            <i className="fa fa-pencil"></i>
                        </a>
                        {hiddenInputs}
                    </td>

                </tr>
            );
        }, self);
    },

    renderHeaders(gridHeaders) {
        if (!gridHeaders) {
            return (
                <th> Oops something happened!</th>
            );
        }


        return gridHeaders.map(function (item, index) {
            return (
                <th key={index}> {item.Header}</th>
            );
        });
    },

    render() {

        if (!this.state.data) {
            return (null);
        }
        var currentGrid = this.state.data.Grid[0];
        var headers = this.renderHeaders(currentGrid.GridHeader);
        var rowData = this.renderRows(currentGrid.GridRow, currentGrid.NumberOfColumns, currentGrid.DataId);



        return (
            <table className="-grid" >
                <thead>
                    <tr>
                        {headers}
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {rowData}
                </tbody>
            </table >

        );
    }
});