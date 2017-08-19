var LabelValueDropDownWithValidation = React.createClass({
	propTypes: {
		showError: React.PropTypes.bool.isRequired,
		onFieldChanged: React.PropTypes.func.isRequired,
		dataBindName: React.PropTypes.string.isRequired
	},

	getInitialState: function () {
		return (null);
	},

	errorsToDisplay() {
		//currently we only display one at a time.
		return this.props.errorList[this.props.dataBindName] || "";

	},

	shouldDisplayError() {
		return this.props.showError && this.errorsToDisplay() !== "";
	},


	render: function () {
		if (this.props.items
			&& this.props.items.length === 0) {
			return (null);
		}
		var self = this;

		var opts = [];


		if (this.props.items) {
			opts = this.props.items.map(function (option) {
				return (
					<option key={option[self.props.valueField]} value={option[self.props.valueField]}>
						{option[self.props.labelField]}
					</option>
				);
			});
		}
		
		var selectedValue = '';
		if (this.props.selected) {
			selectedValue = this.props.selected;
		}

		return (
			<div >
				<select
					id={this.props.id}
					name={this.props.name || this.props.id}
					className='form-control'
					value={selectedValue}
					onChange={this.handleChange}
					data-bind-name={this.props.dataBindName}>
					<option value="">Choose your option...</option>
					{opts}
				</select>
				<ErrorView display={this.shouldDisplayError()}>
					<div className="validation-error" style={{ color: 'red' }}>
						<span className="text">{this.errorsToDisplay()}</span>
					</div>
				</ErrorView>
			</div >
		);
	},

	handleChange: function (e) {
		if (this.props.onFieldChanged) {
			this.props.onFieldChanged(e);
		}
	}
});