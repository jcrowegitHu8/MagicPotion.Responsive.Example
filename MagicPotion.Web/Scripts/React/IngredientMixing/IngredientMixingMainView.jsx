var IngredientMixingMainView = React.createClass({
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
	

	componentDidMount: function () {
		this.handleRefresh();
	},


	render: function () {

		return (

			<div>
				<LoadingModalView show={this.state.showLoadingBox} />
				<IngredientMixingTool
					ingredients={this.state.initData.Ingredients}
					moods={this.state.initData.Moods}
					effects={this.state.initData.Effects}
					mixes={this.state.initData.IngredientMixes}
					submitMixUrl="/IngredientMixing/Mix"
				/>
			</div>
		);
	}
});

var targetElement = document.getElementById("ingredientMixingMainView");
if (targetElement) {
	ReactDOM.render(
		<IngredientMixingMainView
			getInitDataUrl="/IngredientMixing/GetInitData"
		/>,
		targetElement
	);
};