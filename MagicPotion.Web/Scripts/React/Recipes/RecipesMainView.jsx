var RecipesMainView = React.createClass({
	getInitialState: function () {
		return {
			initData: [],
			showLoadingBox: false,
			editRecipeModalData: {
				show: false,
				editId: null,
				title: 'Edit Recipe',
            },
			confirmDialogData: {
                show: false,
				eventSource: null
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

    handleConfirmDeleteRecipe(e) {
	    var id = e.target.closest('tr').getAttribute('data-id');
        this.state.confirmDialogData.show = true;
        this.state.confirmDialogData.eventSource = id;
		this.setState({confirmDialogData:this.state.confirmDialogData});
    },
	handleCancelOrCloseDialog() {
		this.state.confirmDialogData.show = false;
		this.setState({ confirmDialogData: this.state.confirmDialogData });
	},

    handleDeleteRecipe(id) {
	    
		var xhr = new XMLHttpRequest();
        xhr.open('POST', this.props.deleteRecipeUrl + '?id='+id, true);
		xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
		this.setState({ showLoadingBox: true }, function() {
			xhr.onreadystatechange = function() {
				if (xhr.readyState === 4 && xhr.status === 200) {
                    this.props.handleCancelOrCloseDialog();
				}

			}.bind(this);
			xhr.send();
		}.bind(this));
	},

	

	componentDidMount: function () {
		this.handleRefresh();
	},




	render: function () {

		return (

			<div>
                <LoadingModalView show={this.state.showLoadingBox} />
                <ConfirmDialog
                    show={this.state.confirmDialogData.show}
                    title={"Confirm Recipe Delete"}
                    text={"Are you sure you want to delete this Recipe?  The cannot be undone!"}
                    confirmButtonText={"Delete"}
                    onConfirm={this.handleDeleteRecipe}
                    onCancel={this.handleCancelOrCloseDialog}
                    clickSource={this.state.confirmDialogData.eventSource}
                    confirmButtonClassName={"btn btn-danger"}
                    headerClassNames={"danger"}
                />
				<RecipesResultView
					data={this.state.initData}
					refresh={this.handleRefresh}
					onEditRecipe={this.handleEditRecipe}
                    onAddRecipe={this.handleAddRecipe}
                    onDeleteRecipe={this.handleConfirmDeleteRecipe}
				/>
				<RecipeEditModal
					title={this.state.editRecipeModalData.title}
					editId={this.state.editRecipeModalData.editId}
					show={this.state.editRecipeModalData.show}
					effectsUrl={this.props.getEffectsUrl}
					moodsUrl={this.props.getMoodsUrl}
                    editUrl={this.props.editRecipeUrl}
                    updateUrl={this.props.saveRecipeUrl}
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
			saveRecipeUrl="/Recipe/Save"
			deleteRecipeUrl="/Recipe/Delete"
			getEffectsUrl="/Type/GetEffectsList"
			getMoodsUrl="/Type/GetMoodsList"
		/>,
		targetElement
	);
};