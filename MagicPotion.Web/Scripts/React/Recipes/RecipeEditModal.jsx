
var RecipeEditModal = React.createClass({
	propTypes: {
		validationRules: React.PropTypes.array
	},

	getDefaultProps() {
		return {
			validationRules: [
				ruleRunner("Recipe.Name", "Name", requiredRule, minLengthRule(3)),
				ruleRunner("Recipe.MoodType", "Mood", requiredRule),
				ruleRunner("Recipe.EffectType", "Effect", requiredRule),
			]
		};
	},

	getInitialState() {
		return {
			showLoadingBox: false,
			data: {
				"Recipe": {
					"Id": 0,
					"Name": "",
					"IsFatal": false,
					"MoodType": 0,
					"Mood": "",
					"EffectType": 0,
					"Effect": "",
					"Ingredients": [{
						"Predecessor": null,
						"RecipeId": 0,
						"Id": 0,
						"Name": "",
					}]
				},
				"Moods": [],
				"Effects": [],
				"Ingredients": []
			},
			effects: [],
			showErrors: false,
			validationErrors: {},
		}
	},

	errorFor(field) {
		return this.state.validationErrors[field] || "";
	},

	runValidation() {
		var state = this.state;
		var data = this.state.data;
		state.validationErrors = run(data, this.props.validationRules);
		this.setState(state);

	},

	handleSubmitClicked(e) {
		this.runValidation();
		this.setState({ showErrors: true });
		if ($.isEmptyObject(this.state.validationErrors) === false) return null;
		return this.handleSubmit(e);
	},

	handleSubmit: function (e) {
		e.preventDefault();
		return;
		var postModel = this.state.data.Recipe;
		var xhr = new XMLHttpRequest();
		xhr.open('post', this.props.editUrl, true);
		xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
		this.setState({ showLoadingBox: true }, function () {
			xhr.onreadystatechange = function () {
				if (xhr.readyState == 4 && xhr.status == 200) {
					this.props.onClose(true);
				}

			}.bind(this);
			xhr.send(JSON.stringify(postModel));
		}.bind(this));
	},

	shouldComponentUpdate(nextProps, nextState) {
		// return a boolean value
		if (this.props.editId != nextProps.editId) {
			this.handleRefreshRecipeData();
		}
		return true;
	},

	handleRefreshRecipeData() {
		var url = this.props.editUrl;
		var id = this.props.parentState.editId;
		if (id && id.length > 0) {
			url += '?id=' + id;
		}

		var xhr = new XMLHttpRequest();
		xhr.open('get', url, true);
		xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
		this.setState({ showLoadingBox: true }, function () {
			xhr.onload = function () {
				var data = JSON.parse(xhr.responseText);
				this.setState({
					data: data
					, showLoadingBox: false
					, showErrors: false
					, validationErrors: {}
				});
			}.bind(this);
			xhr.send();
		}.bind(this));
	},

	handleChange(e) {
		var data = this.state.data;
		var bindName = e.target.getAttribute('data-bind-name');
		DataBindHelper.assign(data, bindName, e.target.value);
		this.setState({ data });
	},

	isFieldRequired(bindName) {
		if (!this.props.validationRules)
			return '';

		var result = this.props.validationRules.filter(function (rule) { return rule.dataField === bindName });
		if (result.length > 0) {
			return 'requiredField';
		}
		return '';
	},

	renderTextAreaValidationControl(id, bindName, initialValue, labelText) {
		var required = this.isFieldRequired(bindName);

		return (
			<div className="form-group">
				<label className={required} >{labelText}</label>

				<TextAreaWithValidation
					className="form-control"
					id={id}
					dataBindName={bindName}
					showError={this.state.showErrors}
					text={initialValue}
					onFieldChanged={this.handleChange}
					errorList={this.state.validationErrors}
				/>
			</div>
		);
	},

	renderTextValidationControl(id, bindName, initialValue, labelText) {
		var required = this.isFieldRequired(bindName);

		return (
			<div className="form-group">
				<label className={required} >{labelText}</label>

				<TextWithValidation
					className="form-control"
					id={id}
					dataBindName={bindName}
					showError={this.state.showErrors}
					text={initialValue}
					onFieldChanged={this.handleChange}
					errorList={this.state.validationErrors}
				/>
			</div>
		);
	},

	renderDropDownValidationControl(id, bindName, initialValue, items, labelText) {
		var required = this.isFieldRequired(bindName);
		return (
			<div className="form-group">
				<label className={required}>{labelText}</label>
				<LabelValueDropDownWithValidation
					id={id}
					titleClass="form-control"
					optionClass=""
					selectClass=""
					show={true}
					items={items}
					valueField="Id"
					labelField="Name"
					onFieldChanged={this.handleChange}
					selected={initialValue}
					dataBindName={bindName}
					showError={this.state.showErrors}
					errorList={this.state.validationErrors}
				/>
			</div>
		);
	},

	getDisplayStyle() {
		return this.props.parentState.show ? "block" : "none";
	},

	getIngredientsToAdd() {
		var self = this;
		if (!this.state.data || !this.props.parentState.show) {
			return [];
		}
		if (!this.state.data.Recipe || !this.state.data.Recipe.Ingredients) {
			return this.state.data.Ingredients;
		}

		var result = _.reject(self.state.data.Ingredients, function (item) {
			return _.find(self.state.data.Recipe.Ingredients, { IngredientId: item.Id });
		});
		return result;
	},

	handleDeleteIngredient(e) {
		var ingredientId = e.target.closest("div[data-recipe-ingredient-id]").getAttribute('data-recipe-ingredient-id');
		if (ingredientId) {
			var self = this;
			this.state.data.Recipe.Ingredients = _.without(self.state.data.Recipe.Ingredients,
				_.findWhere(self.state.data.Recipe.Ingredients, {
					IngredientId: parseInt(ingredientId)
				}));
			this.setState({ data: this.state.data });
		}
	},

	handleAddIngredient() {
		
		var ingredientId = $('#newIngredient option:selected').val();
		if (ingredientId) {
			var ingredient = _.find(this.state.data.Ingredients, function (item) { return item.Id == ingredientId; });
			var newRecipeIngredient = { IngredientId: ingredient.Id, Name: ingredient.Name }
			this.state.data.Recipe.Ingredients.push(newRecipeIngredient);
			this.setState({ data: this.state.data });
		}
	},

	renderIngredientDetailViews() {
		if (!this.state.data.Recipe.Ingredients) {
			return (null);
		}
		if (this.state.data.Recipe.Ingredients.length === 0) {
			return(
				<p>No Ingredients have been added.</p>
			);
		}

		return this.state.data.Recipe.Ingredients.map(function (info, index) {
			return (
				<div className="row rounded" key={info.IngredientId} data-recipe-ingredient-id={info.IngredientId}>
					<div className="col-xs-12">
						<div className=""
							style={{ lineHeight: '38px', verticalAlign: 'middle', paddingTop: '2px' }}>{info.Name}
							<button className="btn btn-danger btn-outline pull-right" onClick={this.handleDeleteIngredient}>
								<i className="fa fa-trash"></i></button>
						</div>
					</div>

				</div>
			);
		}, this);
	},

	renderIngredientSection() {

		return (
			<div className="panel panel-default">
				<div className={'panel-heading clearfix'}>
					<div>
						<span className="panel-title">Ingredients</span>

					</div>
				</div>
				<div className="panel-body">
					{this.renderIngredientDetailViews()}
				</div>
				<div className="panel-footer">
					<div className="form-group input-group">
						<LabelValueDropdown
							id={"newIngredient"}
							titleClass="form-control"
							optionClass=""
							optionText="Choose an Ingredient to add..."
							selectClass=""
							show={true}
							items={this.getIngredientsToAdd()}
							valueField="Id"
							labelField="Name"
							onFieldChanged={this.handleChange}
							selected={""}
							dataBindName={""}

						/>
						<span className="input-group-btn">
							<button className="btn btn-outline btn-primary"
								onClick={this.handleAddIngredient}
								title="add">
								<i className="fa fa-plus"></i>
							</button>
						</span>
					</div>
				</div>
			</div>
		);
	},

	renderModalbody() {
		return (
			<div>
				<div className="">
					<form onSubmit={this.handleSubmitClicked}>

						<div className="row">
							<div className="col-md-6 col-lg-4">
								{this.renderTextValidationControl("Name"
									, "Recipe.Name"
									, this.state.data.Recipe.Name
									, "Name")}

							</div>
							<div className="col-md-6 col-lg-4">
								{this.renderDropDownValidationControl("MoodType",
									"Recipe.MoodType"
									, this.state.data.Recipe.MoodType
									, this.state.data.Moods
									, "Mood")}
							</div>
							<div className="col-md-6 col-lg-4">
								{this.renderDropDownValidationControl("EffectType",
									"Recipe.EffectType"
									, this.state.data.Recipe.EffectType
									, this.state.data.Effects
									, "Effect")}
							</div>
						</div>

						<div className="row">
							<div className="col-xs-12">
								{this.renderIngredientSection()}
							</div>
						</div>
					</form>
				</div>
			</div>

		);
	},

	render() {

		return (
			<div className="modal" style={{ display: this.getDisplayStyle() }}
				tabIndex="-1" role="dialog">
				<div className="modal-dialog modal-lg" role="dialog">
					<div className="modal-content">
						<div className={'modal-header'}>
							<button type="button"
								className="close"
								data-dismiss="modal"
								aria-label="Close"
								onClick={this.props.onClose}>
								<span aria-hidden="true">&times;</span>
							</button>
							<h4 className="modal-title">{this.props.parentState.title}</h4>
						</div>
						<div className="modal-body">
							{this.renderModalbody()}
						</div>
						<div className="modal-footer">
							<button
								type="button"
								className="btn btn-link"
								data-dismiss="modal"
								onClick={this.props.onClose}>
								Cancel
							</button>
							<button
								type="button"
								className="btn btn-primary"
								data-dismiss="modal"

								onClick={this.handleSubmitClicked}>
								Save
							</button>
						</div>
					</div>
				</div>
			</div>
		);
	},

});

