var RecipeEditModal = React.createClass({
	propTypes: {
		labelText: React.PropTypes.string.isRequired,
		initialValue: React.PropTypes.string.isRequired,
		dataBindName: React.PropTypes.string.isRequired,
		showErrors: React.PropTypes.string.isRequired,
	},

	render() {
		var required = this.isFieldRequired(this.props.dataBindName);


		return (
			<div className="form-group">
				<label className={required} >{this.props.labelText}</label>

				<TextWithValidation
					className="form-control"
					id={id}
					dataBindName={this.props.dataBindName}
					showError={this.state.showErrors}
					text={this.props.initialValue}
					onFieldChanged={this.handleChange}
					errorList={this.state.validationErrors}
				/>
			</div>
		);
	},

});