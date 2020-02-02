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

import MenuContainer from '../../containers/MenuContainer'

class Position extends React.Component {

  constructor(props) {
    super(props);

    this.state = {
      isLoaded: false
    };
  }

  componentDidMount() {
    this.props.getAllPositions().then((result) => {
      this.setState({ isLoaded: true });
    });
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
                  endIcon={<AddIcon />}>
                  Add
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

export default Position
