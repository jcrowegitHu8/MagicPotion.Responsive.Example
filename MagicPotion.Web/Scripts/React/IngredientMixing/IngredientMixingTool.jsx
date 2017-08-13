
var IngredientMixingTool = React.createClass({
	getInitialState: function () {
		return {
			userMixData: {},
			mixResult: {},
			mixing: false,
			disableMixSubmit: true
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
			this.state.userMixData.ingredientId1 &&
			this.state.userMixData.ingredientId2) {
			this.setState({ disableMixSubmit: false });

		} else {
			this.setState({ disableMixSubmit: true });
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
		var data = this.state;
		var bindName = e.target.getAttribute('data-bind-name');
		this.assign(data, bindName, e.target.value);
		this.handleMixing();
		this.setState({ mixResult: {} });
	},

	renderMixResult() {
		if (this.state.mixing) {
			return (<div>
				<i className="fa fa-flask fa-pulse" style={{ color: 'green' }}
					title="mixing"></i> &nbsp;Mixing...
			        </div>);
		}

		if (this.state.mixResult) {


			if (this.state.mixResult.IsMixFatal) {


				if (!this.state.mixResult.IsMixDocumented) {
					return (
						<div className="alert alert-danger">
							BOOM! ¯\_(ツ)_/¯ Looks like an explosion happened. Mixing undocumented ingredients can do that.
						</div>);
				} else {
					return (
						<div className="alert alert-danger">
							You mixed a fatal substance and died. Please be more careful next time. ¯\_(ツ)_/¯
						</div>);
				}
			} else {
				if (this.state.mixResult.IsMixDocumented) {
					if (this.state.userMixData.moodId === "2") {//sad
						return (
							<div className="alert alert-warning">
								You just exacerbated your: {this.state.mixResult.Effect}
							</div>);
					} else {

						return (
							<div className="alert alert-success">
								You just leveled up your: {this.state.mixResult.Effect}
							</div>
						);
					}
				}

			}
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
						<div className="panel-body" >
							<form role="form">
								<div className="row">
									<div className="col-sm-6 col-md-4">
										<div className="form-group">
											<label>How are you feeling right now?</label>
											<LabelValueDropdown
												items={this.props.moods}
												onChange={this.handleChange}
												labelField={"Name"}
												valueField={"Id"}
												dataBindName={'userMixData.moodId'} />
										</div>
									</div>
									<div className="col-sm-6 col-md-4">
										<div className="form-group">
											<label>Ingredient 1</label>
											<LabelValueDropdown
												items={this.props.ingredients}
												onChange={this.handleChange}
												labelField={"Name"}
												valueField={"Id"}
												dataBindName={'userMixData.ingredientId1'} />
										</div>
									</div>
									<div className="col-sm-6 col-md-4">
										<div className="form-group">
											<label>Ingredient 2</label>
											<LabelValueDropdown
												items={this.props.ingredients}
												onChange={this.handleChange}
												labelField={"Name"}
												valueField={"Id"}
												dataBindName={'userMixData.ingredientId2'} />
										</div>
									</div>
								</div>

								<div className="row">
									<div className="col-xs-12">
										<div className="form-group">
											<label>Result</label>
											{this.renderMixResult()}
										</div>
									</div>
								</div>
							</form>
						</div>
						<div className="panel-footer clearfix">
							<div className="pull-right">
								<button className="btn btn-primary " onClick={this.submitMix} disabled={this.state.disableMixSubmit}>
									<i className="fa fa-flask"></i> Mix
								</button>
							</div>
						</div>
					</div>

				</div>
			</div>

		);

	}
});