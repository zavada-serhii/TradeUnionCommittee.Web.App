import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import CircularProgress from '@material-ui/core/CircularProgress';
import Button from '@material-ui/core/Button';
import AddIcon from '@material-ui/icons/Add';
import { withRouter } from "react-router-dom";
import { APP_CREATE_POSITION } from '../../constants/routes'

import MenuContainer from '../../containers/MenuContainer'

class Position extends React.Component {

  constructor(props) {
    super(props);

    this.state = {
      isLoaded: false
    };

    this.redirectToCreatePosition = this.redirectToCreatePosition.bind(this);
  }

  componentDidMount() {
    this.props.getAllPositions().then((result) => {
      this.setState({ isLoaded: true });
    });
  }

  redirectToCreatePosition(event) {
    event.preventDefault()
    this.props.history.push(APP_CREATE_POSITION)
  }

  render() {

    const { positions } = this.props;
    const { isLoaded } = this.state;

    return (
      <TableContainer>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell><strong>Name</strong></TableCell>
              <TableCell align="right">
                <Button
                  variant="contained"
                  color="primary"
                  onClick={this.redirectToCreatePosition}
                  endIcon={<AddIcon />}>
                  Create
                </Button>
              </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>

            {!isLoaded &&
              <TableRow>
                <TableCell colSpan="2" align="center">
                  <CircularProgress />
                </TableCell>
              </TableRow>
            }

            {isLoaded && positions.length !== 0 &&
              positions.map(row => (
                <TableRow key={row.name}>
                  <TableCell component="th" scope="row">
                    {row.name}
                  </TableCell>
                  <TableCell align="right">
                    <MenuContainer />
                  </TableCell>
                </TableRow>
              ))
            }

            {isLoaded && positions.length === 0 &&
              <TableRow>
                <TableCell colSpan="2" align="center">
                  Positions are empty
                </TableCell>
              </TableRow>
            }

          </TableBody>
        </Table>
      </TableContainer>
    );
  }
}

export default withRouter(Position)
