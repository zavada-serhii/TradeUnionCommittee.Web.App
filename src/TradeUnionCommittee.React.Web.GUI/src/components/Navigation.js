import React from 'react';
import clsx from 'clsx';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import List from '@material-ui/core/List';
import CssBaseline from '@material-ui/core/CssBaseline';
import Typography from '@material-ui/core/Typography';
import Divider from '@material-ui/core/Divider';
import IconButton from '@material-ui/core/IconButton';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import Collapse from '@material-ui/core/Collapse';
import { Link } from "react-router-dom";
import { Route, Switch } from 'react-router-dom'

import MenuIcon from '@material-ui/icons/Menu';
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import ChevronRightIcon from '@material-ui/icons/ChevronRight';
import ExpandLess from '@material-ui/icons/ExpandLess';
import ExpandMore from '@material-ui/icons/ExpandMore';
import PersonAddIcon from '@material-ui/icons/PersonAdd';
import CategoryIcon from '@material-ui/icons/Category';
import BarChartIcon from '@material-ui/icons/BarChart';
import AssessmentIcon from '@material-ui/icons/Assessment';
import AccessibleIcon from '@material-ui/icons/Accessible';
import AttachMoneyIcon from '@material-ui/icons/AttachMoney';
import AccessibilityIcon from '@material-ui/icons/Accessibility';
import GolfCourseIcon from '@material-ui/icons/GolfCourse';
import TodayIcon from '@material-ui/icons/Today';
import EventIcon from '@material-ui/icons/Event';
import EventAvailableIcon from '@material-ui/icons/EventAvailable';
import EventNoteIcon from '@material-ui/icons/EventNote';
import DateRangeIcon from '@material-ui/icons/DateRange';
import SubdirectoryArrowRightIcon from '@material-ui/icons/SubdirectoryArrowRight';
import HomeWorkIcon from '@material-ui/icons/HomeWork';
import HotelIcon from '@material-ui/icons/Hotel';
import PeopleIcon from '@material-ui/icons/People';
import DashboardIcon from '@material-ui/icons/Dashboard';
import StorageIcon from '@material-ui/icons/Storage';
import SearchIcon from '@material-ui/icons/Search';
import ExitToAppIcon from '@material-ui/icons/ExitToApp';

import CounterContainer from '../containers/CounterContainer'

const drawerWidth = 280;

const useStyles = makeStyles(theme => ({
  root: {
    display: 'flex',
  },
  appBar: {
    zIndex: theme.zIndex.drawer + 1,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
  },
  appBarShift: {
    marginLeft: drawerWidth,
    width: `calc(100% - ${drawerWidth}px)`,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  },
  menuButton: {
    marginRight: 36,
  },
  hide: {
    display: 'none',
  },
  drawer: {
    width: drawerWidth,
    flexShrink: 0,
    whiteSpace: 'nowrap',
  },
  drawerOpen: {
    width: drawerWidth,
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  },
  drawerClose: {
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
    overflowX: 'hidden',
    width: theme.spacing(7) + 1,
    [theme.breakpoints.up('sm')]: {
      width: theme.spacing(9) + 1,
    },
  },
  toolbar: {
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'flex-end',
    padding: theme.spacing(0, 1),
    ...theme.mixins.toolbar,
  },
  content: {
    flexGrow: 1,
    marginTop: '64px',
    padding: theme.spacing(3),
  },
  nested: {
    paddingLeft: theme.spacing(4),
  },
}));

export default function MiniDrawer() {
  const classes = useStyles();
  const theme = useTheme();
  const [openPanel, setOpenPanel] = React.useState(false);
  const [openDirectories, setOpenDirectories] = React.useState(false);

  const handleDrawerOpen = () => {
    setOpenPanel(true);
  };

  const handleDrawerClose = () => {
    setOpenPanel(false);
    setOpenDirectories(false);
  };

  const handleClickDirectories = () => {
    if(openPanel){
      setOpenDirectories(!openDirectories);
    }
  };

  return (
    <div className={classes.root}>
        <CssBaseline />
        <AppBar
          position="fixed"
          className={clsx(classes.appBar, {
            [classes.appBarShift]: openPanel,
          })}>

          <Toolbar>
            <IconButton
              color="inherit"
              aria-label="openPanel drawer"
              onClick={handleDrawerOpen}
              edge="start"
              className={clsx(classes.menuButton, {
                [classes.hide]: openPanel,
              })}>
              <MenuIcon />
            </IconButton>
            <Typography variant="h6" noWrap>
              TradeUnionCommittee.React.Web.GUI
          </Typography>
          </Toolbar>
        </AppBar>
        <Drawer
          variant="permanent"
          className={clsx(classes.drawer, {
            [classes.drawerOpen]: openPanel,
            [classes.drawerClose]: !openPanel,
          })}
          classes={{
            paper: clsx({
              [classes.drawerOpen]: openPanel,
              [classes.drawerClose]: !openPanel,
            }),
          }}>

          <div className={classes.toolbar}>
            <IconButton onClick={handleDrawerClose}>
              {theme.direction === 'rtl' ? <ChevronRightIcon /> : <ChevronLeftIcon />}
            </IconButton>
          </div>
          <Divider />

          <List>

            <ListItem button component={Link} to="/create-employee" title="Create new employee">
              <ListItemIcon>
                <PersonAddIcon />
              </ListItemIcon>
              <ListItemText primary="Create new employee" />
            </ListItem>

            <ListItem button onClick={handleClickDirectories} title="Directories">
              <ListItemIcon>
                <CategoryIcon />
              </ListItemIcon>
              <ListItemText primary="Directories" />
              {openDirectories ? <ExpandLess /> : <ExpandMore />}
            </ListItem>

            <Collapse in={openDirectories} timeout="auto" unmountOnExit>
              <List component="div" disablePadding>

                <ListItem button className={classes.nested} component={Link} to="/position">
                  <ListItemIcon>
                    <BarChartIcon />
                  </ListItemIcon>
                  <ListItemText primary="Position" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to="/social-position">
                  <ListItemIcon>
                    <AssessmentIcon />
                  </ListItemIcon>
                  <ListItemText primary="Social position" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to="/privileges">
                  <ListItemIcon>
                    <AccessibleIcon />
                  </ListItemIcon>
                  <ListItemText primary="Privileges" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to="/award">
                  <ListItemIcon>
                    <AttachMoneyIcon />
                  </ListItemIcon>
                  <ListItemText primary="Award" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to="/material-aid">
                  <ListItemIcon>
                    <AccessibilityIcon />
                  </ListItemIcon>
                  <ListItemText primary="Material Aid" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to="/hobby">
                  <ListItemIcon>
                    <GolfCourseIcon />
                  </ListItemIcon>
                  <ListItemText primary="Hobby" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to="/travel">
                  <ListItemIcon>
                    <TodayIcon />
                  </ListItemIcon>
                  <ListItemText primary="Travel" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to="/wellness">
                  <ListItemIcon>
                    <EventIcon />
                  </ListItemIcon>
                  <ListItemText primary="Wellness" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to="/tour">
                  <ListItemIcon>
                    <EventAvailableIcon />
                  </ListItemIcon>
                  <ListItemText primary="Tour" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to="/activities">
                  <ListItemIcon>
                    <EventNoteIcon />
                  </ListItemIcon>
                  <ListItemText primary="Activities" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to="/cultural-activities">
                  <ListItemIcon>
                    <DateRangeIcon />
                  </ListItemIcon>
                  <ListItemText primary="Cultural activities" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to="/subdivisions">
                  <ListItemIcon>
                    <SubdirectoryArrowRightIcon />
                  </ListItemIcon>
                  <ListItemText primary="Subdivisions" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to="/departmental-housing">
                  <ListItemIcon>
                    <HomeWorkIcon />
                  </ListItemIcon>
                  <ListItemText primary="Departmental housing" />
                </ListItem>

                <ListItem button className={classes.nested} component={Link} to="/dormitory">
                  <ListItemIcon>
                    <HotelIcon />
                  </ListItemIcon>
                  <ListItemText primary="Dormitory" />
                </ListItem>

              </List>
            </Collapse>

            <ListItem button component={Link} to="/users" title="Users">
              <ListItemIcon>
                <PeopleIcon />
              </ListItemIcon>
              <ListItemText primary="Users" />
            </ListItem>

            <ListItem button component={Link} to="/dashboard" title="Dashboard">
              <ListItemIcon>
                <DashboardIcon />
              </ListItemIcon>
              <ListItemText primary="Dashboard" />
            </ListItem>

            <ListItem button component={Link} to="/action-log" title="Action log">
              <ListItemIcon>
                <StorageIcon />
              </ListItemIcon>
              <ListItemText primary="Action log" />
            </ListItem>

            <ListItem button component={Link} to="/search" title="Search">
              <ListItemIcon>
                <SearchIcon />
              </ListItemIcon>
              <ListItemText primary="Search" />
            </ListItem>

            <Divider />

            <ListItem button component={Link} to="/logout" title="Logout">
              <ListItemIcon>
                <ExitToAppIcon />
              </ListItemIcon>
              <ListItemText primary="Logout" />
            </ListItem>

          </List>

        </Drawer>

      <div className={classes.content}>
        <Switch>
          <Route path="/create-employee" component={CounterContainer} />
          <Route path="/position" render={() => <div>Here will be page for Position</div>} />
          <Route path="/social-position" render={() => <div>Here will be page for social-position</div>} />
          <Route path="/privileges" render={() => <div>Here will be page for privileges</div>} />
          <Route path="/award" render={() => <div>Here will be page for award</div>} />
          <Route path="/material-aid" render={() => <div>Here will be page for material-aid</div>} />
          <Route path="/hobby" render={() => <div>Here will be page for hobby</div>} />
          <Route path="/travel" render={() => <div>Here will be page for travel</div>} />
          <Route path="/wellness" render={() => <div>Here will be page for wellness</div>} />
          <Route path="/tour" render={() => <div>Here will be page for tour</div>} />
          <Route path="/activities" render={() => <div>Here will be page for activities</div>} />
          <Route path="/cultural-activities" render={() => <div>Here will be page for cultural-activities</div>} />
          <Route path="/subdivisions" render={() => <div>Here will be page for subdivisions</div>} />
          <Route path="/departmental-housing" render={() => <div>Here will be page for departmental-housing</div>} />
          <Route path="/dormitory" render={() => <div>Here will be page for dormitory</div>} />
          <Route path="/users" render={() => <div>Here will be page for users</div>} />
          <Route path="/dashboard" render={() => <div>Here will be page for dashboard</div>} />
          <Route path="/action-log" render={() => <div>Here will be page for action-log</div>} />
          <Route path="/search" render={() => <div>Here will be page for search</div>} />
          <Route path="/logout" render={() => <div>Here will be page for logout</div>} />
        </Switch>
      </div>
      
    </div>
  );
}
