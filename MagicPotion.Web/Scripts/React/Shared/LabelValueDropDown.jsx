var LabelValueDropdown = React.createClass({

	propTypes: {
		items: React.PropTypes.array.isRequired,
		selectedItem: React.PropTypes.object,
		valueField: React.PropTypes.string,
		labelField: React.PropTypes.string,
		onChange: React.PropTypes.func
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

		var selectedValue = null;
		if (this.props.selectedItem) {
			selectedValue = this.props.selectedItem[this.props.valueField]
		}

		return (
			<select
				className='form-control'
				value={selectedValue}
				onChange={this.handleChange}
				id={this.props.id || this.props.dataBindName}
				data-bind-name={this.props.bindName || this.props.dataBindName}>
				<option value="">{this.props.optionText || 'Choose your option...'}</option>
				{opts}
			</select>
		);
	},

	handleChange: function (e) {
		if (this.props.selectedItem) {
			var selectedItem = this.props.selectedItem;
			selectedItem[this.props.valueField] = e.target.value;
			selectedItem[this.props.labelField] = e.target.items[e.target.selectedIndex].text;
		}
		if (this.props.onChange) {
			this.props.onChange(e);
		}
	}

});
