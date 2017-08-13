var DropDown = React.createClass({
	getInitialState: function() {
		return (null);
	},

	//propTypes: {
	//	show: React.PropTypes.bool,
	//	items: React.PropTypes.array.isRequired,
	//	onChange: React.PropTypes.func
	//},

	//defaultProps: {
	//	show: true,
	//	items:[],
	//},


	handleChange: function (e) {
		this.props.handleChange(e);
	},

	render: function () {
		if (this.props.show === false) {
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
			<select className="{this.props.selectClass} form-control" value={this.props.selected} onChange={this.handleChange}
			        data-bind-name={this.props.bindName || this.props.dataBindName}>
				<option className={this.props.optionClass} value="">Choose your option...</option>
				{opts}
			</select>
		);


	}
});