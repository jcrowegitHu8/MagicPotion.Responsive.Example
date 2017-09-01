var GenericReactGridMainView = React.createClass({
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
		//this.handleRefresh();
	},


	render: function () {

		return (

			<div>
				<LoadingModalView show={this.state.showLoadingBox} />

				<div className="alert alert-info">
					<p>The below grid is generic in that the server side code populates
						an object that specifies the Grid, GridHeaders, Rows
						to populate the table you see.</p>
				</div>

				<GenericReactGridDetailView
					dataUrl={"/GenericGrid/GetData"}
					refreshSelector={null}
				/>

				<div className="alert alert-info">
					<p>The below grid is generic in that the server side code populates
						an object that specifies the Grid, GridHeaders, Rows, SubGrid, SubGridHeaders, SubGridRows
						to populate the table you see.</p>
				</div>
				<GenericReactGridDetailView
					dataUrl={"/GenericGrid/GetDataWithSubGrid"}
					refreshSelector={null}
				/>
			</div>
		);
	}
});

var targetElement = document.getElementById("genericReactGridMainView");
if (targetElement) {
	ReactDOM.render(
		<GenericReactGridMainView/>,
		targetElement
	);
};