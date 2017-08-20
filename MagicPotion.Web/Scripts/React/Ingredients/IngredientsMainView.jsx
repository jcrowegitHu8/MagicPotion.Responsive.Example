var IngredientsMainView = React.createClass({
	getInitialState: function () {
		return {
			initData: [],
			showLoadingBox: false,
			editIngredientModalData: {
				show: false,
				editId: null,
				title: 'Edit Ingredient',
			}
		};
	},

	handleRefresh: function () {
		var xhr = new XMLHttpRequest();
		xhr.open('get', this.props.getInitDataUrl, true);
		xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
		this.setState({ showLoadingBox: true }, function () {
			xhr.onload = function () {
				var data = JSON.parse(xhr.responseText);
				this.setState({ initData: data, showLoadingBox: false });

			}.bind(this);
			xhr.send();
		}.bind(this));
	},

	handleEditIngredient: function (e) {
		var id = e.target.closest('tr').getAttribute('data-id');
		this.state.editIngredientModalData.show = true;
		this.state.editIngredientModalData.editId = id;
		this.setState({ editIngredientModalData: this.state.editIngredientModalData });
	},

	handleCloseModal(refresh) {
		this.state.editIngredientModalData.show = false;
		this.setState({ editIngredientModalData: this.state.editIngredientModalData });
		if (refresh === true) {
			this.handleRefresh();
		}
	},

	handleAddIngredient(e) {
		window.location = this.props.editIngredientUrl;
	},

	componentDidMount: function () {
		this.handleRefresh();
	},

	


	render: function () {

		return (

			<div>
				<LoadingModalView show={this.state.showLoadingBox} />
				<IngredientsResultView
					data={this.state.initData}
					refresh={this.handleRefresh}
					onEditIngredient={this.handleEditIngredient}
					add={this.handleAddIngredient}
				/>
				<IngredientEditModal
					parentState={this.state.editIngredientModalData}
					editId={this.state.editIngredientModalData.editId}
					effectsUrl={this.props.getEffectsUrl}
					editUrl={this.props.editIngredientUrl}
					onClose={this.handleCloseModal}
				/>
			</div>
		);
	}
});

var targetElement = document.getElementById("ingredientsMainView");
if (targetElement) {
	ReactDOM.render(
		<IngredientsMainView
			getInitDataUrl="/Ingredient/GetListviewInitData"
			editIngredientUrl="/Ingredient/Edit"
			getEffectsUrl="/Ingredient/GetEffectsList"
		/>,
		targetElement
	);
};