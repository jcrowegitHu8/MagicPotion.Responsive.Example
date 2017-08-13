var IngredientsMainView = React.createClass({
	getInitialState: function () {
		return {
			initData: [],
			showLoadingBox: false,
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
		window.location = this.props.editIngredientUrl + '?id=' + id;
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
					edit={this.handleEditIngredient}
					add={this.handleAddIngredient}
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
		/>,
		targetElement
	);
};