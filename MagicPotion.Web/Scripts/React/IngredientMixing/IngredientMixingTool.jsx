
var IngredientMixingTool = React.createClass({
	getInitialState: function () {
		return {
			userMixData: {},
			mixResult: {},
			mixing:false
		};
	},



	generateDetailViews: function () {
		if (!this.props.data) {
			return (
				<tr><td colSpan={5}>No data returned</td></tr>
			);
		}

		var items = this.props.data;

		return items.map(function (info, index) {
			return (
				<tr key={index} data-id={info.Id} >
					<td data-title={'ID'}>{info.Id}</td>
					<td data-title={'Name'}>{info.Name}&nbsp;</td>
					<td data-title={'Color'}>{info.Color}</td>
					<td data-title={'Description'}>{info.Description}&nbsp;</td>
					<td data-title={'Effect'}>{info.Effect}&nbsp;</td>
				</tr >
			);
		}, this);

	},

	reset: function () {
		this.setState({ userMixData: {} });
	},

	assign(obj, prop, value) {
		if (typeof prop === "string")
			prop = prop.split(".");

		if (prop.length > 1) {
			var e = prop.shift();
			this.assign(obj[e] =
				Object.prototype.toString.call(obj[e]) === "[object Object]"
					? obj[e]
					: {},
				prop,
				value);
		} else
			obj[prop[0]] = value;
	},

	handleMixing() {
		if (this.state.userMixData &&
			this.state.userMixData.moodId &&
			this.state.userMixData.ingredient1 &&
			this.state.userMixData.ingredient2) {
			this.submitMix();

		}
	},

	submitMix: function () {

		var xhr = new XMLHttpRequest();
		xhr.open('post', this.props.submitMixUrl, true);
		xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
		this.setState({ mixing: true }, function () {
			xhr.onload = function () {
				var data = JSON.parse(xhr.responseText);
				this.setState({ mixResult: data, mixing: false });
			}.bind(this);
			xhr.send(JSON.stringify(this.state.userMixData));
		}.bind(this));
	},

	handleChange(e) {
		debugger;
		var data = this.state;
		var bindName = e.target.getAttribute('data-bind-name');
		this.assign(data, bindName, e.target.value);
		this.handleMixing();
	},

	renderMixResult() {
		if (this.state.mixing) {
			return (<div>
				<i className="fa fa-flask fa-pulse" style={{ color: 'green' }}
					title="loading" ></i> &nbsp;Mixing...</div>)
		}

		if (this.state.mixResult && this.state.mixResult.Message) {
			return (
				<div className="alert alert-danger">
					{this.state.mixResult.Message}
				</div>)
		}

		if (this.state.mixResult && this.state.mixResult.Message && this.state.MixResult.SafeMix === "true") {
			return (
				<div className="alert alert-success">
					{this.state.mixResult.Message}
				</div>
			);
		}
	},

	render: function () {



		return (
			<div className="row">
				<div className="col-xs-12">
					<div className="panel panel-default">

						<div className={'panel-heading clearfix'}>

							<div>
								<span className="panel-title" >Ingredients Mixing (Unsafe)</span>

							</div>
						</div>
						<div className="panel-body">
							<div className="row">
								<div className="col-xs-12">
									<div className="form-group">
										<label>How are you feeling right now?</label>
										<LabelValueDropdown
											items={this.props.moods}
											onChange={this.handleChange}
											labelField={"Name"}
											valueField={"Id"}
											dataBindName={'userMixData.moodId'} />
									</div>
									<div className="form-group">
										<label>Ingredient 1</label>
										<LabelValueDropdown
											items={this.props.ingredients}
											onChange={this.handleChange}
											labelField={"Name"}
											valueField={"Id"}
											dataBindName={'userMixData.ingredient1'} />

									</div>
									<div className="form-group">
										<label>Ingredient 2</label>
										<LabelValueDropdown
											items={this.props.ingredients}
											onChange={this.handleChange}
											labelField={"Name"}
											valueField={"Id"}
											dataBindName={'userMixData.ingredient2'} />
									</div>
									<div className="form-group">
										<label>Result</label>
										{this.renderMixResult()}
									</div>
								</div>
							</div>
						</div>
						<div className="panel-footer clearfix">
							<div className="pull-right">
								<button className="btn btn-primary " onClick={this.reset}>
									<i className="fa fa-refresh"></i> Reset
								</button>
							</div>
						</div>
					</div>

				</div>
			</div>

		);

	}
});