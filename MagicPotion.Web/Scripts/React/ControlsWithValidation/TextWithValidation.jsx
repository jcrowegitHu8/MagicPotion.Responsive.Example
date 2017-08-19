var TextWithValidation = React.createClass({

    propTypes: {
        showError: React.PropTypes.bool.isRequired,
        onFieldChanged: React.PropTypes.func.isRequired,
        dataBindName: React.PropTypes.string.isRequired
    },

    errorsToDisplay() {
        //currently we only display one at a time.
        return this.props.errorList[this.props.dataBindName] || "";

    },

    shouldDisplayError() {
        return this.props.showError && this.errorsToDisplay() !== "";
    },

    handleChange: function (event) {
        if (this.props.onFieldChanged) {
            this.props.onFieldChanged(event);
        }
    },

    render() {
        return (
            <div className={this.props.classNames}>
                <input
                    id={this.props.id}
                    name={this.props.name || this.props.id}
                    placeholder={this.props.placeholder}
                    className={this.props.className}
                    type={this.props.type || "text"}
                    data-bind-name={this.props.dataBindName}
                    value={this.props.text} onChange={this.handleChange} />
                <ErrorView display={this.shouldDisplayError()}>
                    <div className="validation-error" style={{ color: 'red' }}>
                        <span className="text">{this.errorsToDisplay()}</span>
                    </div>
                </ErrorView>
            </div>
        );
    }
});


