var RecipesMainView = React.createClass({
	getInitialState: function () {
		return {
			initData: [],
			showLoadingBox: false,
			editRecipeModalData: {
				show: false,
				editId: null,
				title: 'Edit Recipe',
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

	handleEditRecipe: function (e) {
		var id = e.target.closest('tr').getAttribute('data-id');
		this.state.editRecipeModalData.show = true;
		this.state.editRecipeModalData.editId = id;
		this.setState({ editRecipeModalData: this.state.editRecipeModalData });
	},

	handleCloseModal(refresh) {
		this.state.editRecipeModalData.show = false;
		this.setState({ editRecipeModalData: this.state.editRecipeModalData });
		if (refresh) {
			this.handleRefresh();
		}
	},

	handleAddRecipe(e) {
		window.location = this.props.editRecipeUrl;
	},

	componentDidMount: function () {
		this.handleRefresh();
	},




	render: function () {

		return (

			<div>
				<LoadingModalView show={this.state.showLoadingBox} />
				<RecipesResultView
					data={this.state.initData}
					refresh={this.handleRefresh}
					onEditRecipe={this.handleEditRecipe}
					add={this.handleAddRecipe}
				/>
			
			</div>
		);
	}
});

var targetElement = document.getElementById("recipesMainView");
if (targetElement) {
	ReactDOM.render(
		<RecipesMainView
			getInitDataUrl="/Recipe/GetListviewInitData"
			editRecipeUrl="/Recipe/Edit"
			getEffectsUrl="/Type/GetEffectsList"
		/>,
		targetElement
	);
};