var DropDownWithValidation = React.createClass({
    propTypes: {
        showError: React.PropTypes.bool.isRequired,
        onFieldChanged: React.PropTypes.func.isRequired,
        dataBindName: React.PropTypes.string.isRequired
    },

    getInitialState: function () {
        return (null);
    },

    handleChange: function (e) {
        this.props.onFieldChanged(e);
    },

    errorsToDisplay() {
        //currently we only display one at a time.
        return this.props.errorList[this.props.dataBindName] || "";

    },

    shouldDisplayError() {
        return this.props.showError && this.errorsToDisplay() !== "";
    },

    render: function () {
        if (!this.props.show) {
            return (null);
        }

        var opts = [];

        if (this.props && this.props.items) {
            var items = this.props.items;

            opts = items.map(function (item, index) {
                    return (
                        <option key={index} className={this.props.optionClass} value={item}> {item} </option>
                    );
                },
                this);

        }

        return (
            <div>
                <select className="{this.props.selectClass} form-control" value={this.props.selected} onChange={this.handleChange}
                    data-bind-name={this.props.dataBindName}>
                    <option className={this.props.optionClass} value="">Choose your option...</option>
                    {opts}
                </select>

                <ErrorView display={this.shouldDisplayError()}>
                    <div className="validation-error" style={{ color: 'red' }}>
                        <span className="text">{this.errorsToDisplay() }</span>
                    </div>
                </ErrorView>
            </div>
        );


    }
});