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

	handleAddRecipe(e) {
        this.state.editRecipeModalData.show = true;
        this.state.editRecipeModalData.editId = 0;
		this.state.editRecipeModalData.title = 'Add A Recipe';
		this.setState({ editRecipeModalData: this.state.editRecipeModalData });
	},

	handleEditRecipe: function (e) {
		var id = e.target.closest('tr').getAttribute('data-id');
		this.state.editRecipeModalData.show = true;
        this.state.editRecipeModalData.editId = id;
		this.state.editRecipeModalData.title = 'Edit A Recipe';
		this.setState({ editRecipeModalData: this.state.editRecipeModalData });
	},

	handleCloseModal(refresh) {
		this.state.editRecipeModalData.show = false;
		this.setState({ editRecipeModalData: this.state.editRecipeModalData });
		if (refresh === true) {
			this.handleRefresh();
		}
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
				<RecipeEditModal
					title={this.state.editRecipeModalData.title}
					editId={this.state.editRecipeModalData.editId}
					show={this.state.editRecipeModalData.show}
					effectsUrl={this.props.getEffectsUrl}
					moodsUrl={this.props.getMoodsUrl}
                    editUrl={this.props.editRecipeUrl}
                    updateUrl={this.props.editRecipeUrl}
					onClose={this.handleCloseModal}
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
			getEffectsUrl="/Type/GetMoodsList"
		/>,
		targetElement
	);
};