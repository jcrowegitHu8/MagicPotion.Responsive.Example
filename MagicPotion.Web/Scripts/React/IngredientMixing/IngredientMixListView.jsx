
var IngredientMixListView = React.createClass({

	editIngredient: function (id) {
		this.props.handleeditIngredient(id);
	},

	generateDetailViews: function () {
		if (!this.props.data) {
			return (
				<tr><td colSpan={5}>No data returned</td></tr>
			);
		}

		var items = this.props.data;

		return items.map(function (info, index) {
			return (
				<tr key={index} data-id={info.Id} >
					<td data-title={'Ingredient1'}>{info.Ingredient1Name}</td>
					<td data-title={'Ingredient2'}>{info.Ingredient2Name}&nbsp;</td>
					<td data-title={'Mood'}>{info.Mood}&nbsp;</td>
					<td data-title={'Effect'}>{info.Effect}&nbsp;</td>
					<td data-title={'Is Fatal'}>{info.IsFatal.toString()}&nbsp;</td>
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
								<span className="panel-title" >Documented Ingredient Mixes</span>

							</div>
						</div>
						<table className="table table-hover table-bordered table-striped my-responsive-table">
							<thead>
							<tr>
								<th>Ingredient 1</th>
								<th>Ingrediant 2</th>
								<th>Mood</th>
								<th>Effect</th>
								<th>Is Fatal</th>
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