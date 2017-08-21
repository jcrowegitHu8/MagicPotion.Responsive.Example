
var RecipesResultView = React.createClass({

	handleEditRecipe: function (e) {
		this.props.onEditRecipe(e);
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
					<td data-title={'Ingrediant Count'}>{info.IngredientCount}</td>
					<td data-title={'Mood'}>{info.Mood}&nbsp;</td>
					<td data-title={'Effect'}>{info.Effect}&nbsp;</td>
					<td data-title={'Action'}>
						<button className="btn btn-default" onClick={this.handleEditRecipe}>
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
                                <span className="panel-title" >Recipes</span>
								
								<button className="btn btn-outline btn-primary pull-right"
								        onClick={this.props.refresh}
								        title="refresh">
									<i className="fa fa-refresh"></i>
                                </button>
								<button className="btn btn-outline btn-primary pull-right"
								        onClick={this.props.add}
								        title="add">
									<i className="fa fa-plus"></i>
								</button>
							</div>
						</div>
						<table className="table table-hover table-bordered table-striped my-md-responsive-table">
							<thead>
							<tr>
								<th>ID</th>
								<th>Name</th>
								<th>Ingredient Count</th>
								<th>Mood</th>
								<th>Effect</th>
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