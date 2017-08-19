
var IngredientsResultView = React.createClass({

	handleEditIngredient: function (e) {
		this.props.onEditIngredient(e);
	},

	generateDetailViews: function () {
		if (!this.props.data) {
			return (
				<tr><td colSpan={6}>No data returned</td></tr>
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
					<td data-title={'ImportId'}>{info.ImportId}&nbsp;</td>
					<td data-title={'Action'}>
						<button className="btn btn-default" onClick={this.handleEditIngredient}>
							<i className="fa fa-pencil"></i></button>
					</td>
				</tr >
			);
		}, this);

	},

	refresh: function () {
		this.props.refresh();
	},

	render: function () {



		return (
			<div className="row">
				<div className="col-xs-12">
					<div className="panel panel-default">

						<div className={'panel-heading clearfix'}>

							<div>
								<span className="panel-title" >Ingredients</span>
								<button className="btn btn-outline btn-primary pull-right"
								        onClick={this.props.refresh}
								        title="refresh">
									<i className="fa fa-refresh"></i>
								</button>
							</div>
						</div>
						<table className="table table-hover table-bordered table-striped my-md-responsive-table">
							<thead>
								<tr>
									<th>ID</th>
									<th>Name</th>
									<th>Color</th>
									<th>Description</th>
									<th>Effect</th>
									<th>ImportId</th>
									<th></th>
								</tr>
							</thead>
							<tbody>
								{this.generateDetailViews()}
							</tbody>
						</table>
					</div>

				</div>
			</div>

		);

	}
});