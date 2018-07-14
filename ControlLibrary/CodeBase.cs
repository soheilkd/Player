﻿using MaterialDesignThemes.Wpf;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Player.Controls
{
	public enum IconKind
	{
		AccessPoint, AccessPointNetwork, Account, AccountAlert, AccountBox, AccountBoxOutline, AccountCardDetails,
		AccountCheck, AccountCircle, AccountConvert, AccountEdit, AccountKey, AccountLocation, AccountMinus, AccountMultiple, AccountMultipleMinus,
		AccountMultipleOutline, AccountMultiplePlus, AccountMultiplePlusOutline, AccountNetwork, AccountOff, AccountOutline, AccountPlus, AccountPlusOutline, AccountRemove,
		AccountSearch, AccountSettings, AccountSettingsVariant, AccountStar, AccountSwitch, Adjust, AirConditioner, Airballoon, Airplane, AirplaneLanding, AirplaneOff, AirplaneTakeoff, Airplay, Alarm, AlarmBell, AlarmCheck, AlarmLight, AlarmMultiple,
		AlarmOff, AlarmPlus, AlarmSnooze, Album, Alert, AlertBox, AlertCircle, AlertCircleOutline, AlertDecagram, AlertOctagon, AlertOctagram, AlertOutline, AllInclusive, Allo, Alpha, Alphabetical, Altimeter, Amazon,
		AmazonClouddrive, Ambulance, Amplifier, Anchor, Android, AndroidDebugBridge, AndroidHead, AndroidStudio, Angular, Angularjs, Animation, Apple, AppleFinder, AppleIos, AppleKeyboardCaps, AppleKeyboardCommand, AppleKeyboardControl, AppleKeyboardOption,
		AppleKeyboardShift, AppleMobileme, AppleSafari, Application, Approval, Apps, Archive, ArrangeBringForward, ArrangeBringToFront,
		ArrangeSendBackward, ArrangeSendToBack, ArrowAll, ArrowBottomLeft, ArrowBottomRight, ArrowCollapse, ArrowCollapseAll, ArrowCollapseDown, ArrowCollapseLeft,
		ArrowCollapseRight, ArrowCollapseUp, ArrowDown, ArrowDownBold, ArrowDownBoldBox, ArrowDownBoldBoxOutline, ArrowDownBoldCircle, ArrowDownBoldCircleOutline, ArrowDownBoldHexagonOutline,
		ArrowDownBox, ArrowDownDropCircle, ArrowDownDropCircleOutline, ArrowDownThick, ArrowExpand, ArrowExpandAll, ArrowExpandDown, ArrowExpandLeft, ArrowExpandRight,
		ArrowExpandUp, ArrowLeft, ArrowLeftBold, ArrowLeftBoldBox, ArrowLeftBoldBoxOutline, ArrowLeftBoldCircle, ArrowLeftBoldCircleOutline, ArrowLeftBoldHexagonOutline, ArrowLeftBox,
		ArrowLeftDropCircle, ArrowLeftDropCircleOutline, ArrowLeftThick, ArrowRight, ArrowRightBold, ArrowRightBoldBox, ArrowRightBoldBoxOutline, ArrowRightBoldCircle, ArrowRightBoldCircleOutline,
		ArrowRightBoldHexagonOutline, ArrowRightBox, ArrowRightDropCircle, ArrowRightDropCircleOutline, ArrowRightThick, ArrowTopLeft, ArrowTopRight, ArrowUp, ArrowUpBold,
		ArrowUpBoldBox, ArrowUpBoldBoxOutline, ArrowUpBoldCircle, ArrowUpBoldCircleOutline, ArrowUpBoldHexagonOutline, ArrowUpBox, ArrowUpDropCircle, ArrowUpDropCircleOutline, ArrowUpThick,
		Artist, Assistant, Asterisk, At, Atlassian, Atom, Attachment, Audiobook, AutoFix, AutoUpload, Autorenew, AvTimer, Azure, Baby, BabyBuggy, Backburger, Backspace, BackupRestore,
		Bandcamp, Bank, Barcode, BarcodeScan, Barley, Barrel, Basecamp, Basket, BasketFill, BasketUnfill, Basketball, Battery, Battery10, Battery20, Battery30, Battery40, Battery50, Battery60,
		Battery70, Battery80, Battery90, BatteryAlert, BatteryCharging, BatteryCharging100, BatteryCharging20, BatteryCharging30, BatteryCharging40,
		BatteryCharging60, BatteryCharging80, BatteryCharging90, BatteryChargingWireless, BatteryChargingWireless10, BatteryChargingWireless20, BatteryChargingWireless30, BatteryChargingWireless40, BatteryChargingWireless50,
		BatteryChargingWireless60, BatteryChargingWireless70, BatteryChargingWireless80, BatteryChargingWireless90, BatteryChargingWirelessAlert, BatteryChargingWirelessOutline,
		BatteryMinus, BatteryNegative, BatteryOutline, BatteryPlus, BatteryPositive, BatteryUnknown, Beach, Beaker, Beats, Beer, Behance, Bell,
		BellOff, BellOutline, BellPlus, BellRing, BellRingOutline, BellSleep, Beta, Bible, Bike, Bing, Binoculars, Bio, Biohazard, Bitbucket, Bitcoin, BlackMesa, Blackberry, Blender,
		Blinds, BlockHelper, Blogger, Bluetooth, BluetoothAudio, BluetoothConnect, BluetoothOff, BluetoothSettings, BluetoothTransfer,
		Blur, BlurLinear, BlurOff, BlurRadial, Bomb, BombOff, Bone, Book, BookMinus, BookMultiple, BookMultipleVariant, BookOpen, BookOpenPageVariant, BookOpenVariant, BookPlus, BookSecure, BookUnsecure, BookVariant,
		Bookmark, BookmarkCheck, BookmarkMusic, BookmarkOutline, BookmarkPlus, BookmarkPlusOutline, BookmarkRemove, Boombox, Bootstrap,
		BorderAll, BorderBottom, BorderColor, BorderHorizontal, BorderInside, BorderLeft, BorderNone, BorderOutside, BorderRight,
		BorderStyle, BorderTop, BorderVertical, BowTie, Bowl, Bowling, Box, BoxCutter, BoxShadow, Bridge, Briefcase, BriefcaseCheck, BriefcaseDownload, BriefcaseOutline, BriefcaseUpload, Brightness1, Brightness2, Brightness3,
		Brightness4, Brightness5, Brightness6, Brightness7, BrightnessAuto, Broom, Brush, Buffer, Bug, BulletinBoard, Bullhorn, Bullseye, Bus, BusArticulatedEnd, BusArticulatedFront, BusDoubleDecker, BusSchool, BusSide,
		Cached, Cake, CakeLayered, CakeVariant, Calculator, Calendar, CalendarBlank, CalendarCheck, CalendarClock, CalendarMultiple, CalendarMultipleCheck, CalendarPlus, CalendarQuestion, CalendarRange, CalendarRemove, CalendarText, CalendarToday, CallMade,
		CallMerge, CallMissed, CallReceived, CallSplit, Camcorder, CamcorderBox, CamcorderBoxOff, CamcorderOff, Camera, CameraBurst, CameraEnhance, CameraFront, CameraFrontVariant, CameraGopro, CameraIris, CameraMeteringCenter, CameraMeteringMatrix, CameraMeteringPartial,
		CameraMeteringSpot, CameraOff, CameraPartyMode, CameraRear, CameraRearVariant, CameraSwitch, CameraTimer, Cancel, Candle,
		Candycane, Cannabis, Car, CarBattery, CarConnected, CarConvertible, CarEstate, CarHatchback, CarPickup, CarSide, CarSports, CarWash, Caravan, Cards, CardsOutline, CardsPlayingOutline, CardsVariant, Carrot,
		Cart, CartOff, CartOutline, CartPlus, CaseSensitiveAlt, Cash, Cash100, CashMultiple, CashUsd, Cast, CastConnected, CastOff, Castle, Cat, Cctv, CeilingLight, Cellphone, CellphoneAndroid,
		CellphoneBasic, CellphoneDock, CellphoneIphone, CellphoneLink, CellphoneLinkOff, CellphoneSettings, CellphoneWireless, Certificate, ChairSchool,
		ChartArc, ChartAreaspline, ChartBar, ChartBarStacked, ChartBubble, ChartDonut, ChartDonutVariant, ChartGantt, ChartHistogram,
		ChartLine, ChartLineStacked, ChartLineVariant, ChartPie, ChartScatterplotHexbin, ChartTimeline, Check, CheckAll, CheckCircle,
		CheckCircleOutline, CheckboxBlank, CheckboxBlankCircle, CheckboxBlankCircleOutline, CheckboxBlankOutline, CheckboxMarked, CheckboxMarkedCircle, CheckboxMarkedCircleOutline, CheckboxMarkedOutline,
		CheckboxMultipleBlank, CheckboxMultipleBlankCircle, CheckboxMultipleBlankCircleOutline, CheckboxMultipleBlankOutline, CheckboxMultipleMarked, CheckboxMultipleMarkedCircle,
		CheckboxMultipleMarkedCircleOutline, CheckboxMultipleMarkedOutline, Checkerboard, ChemicalWeapon, ChevronDoubleDown, ChevronDoubleLeft, ChevronDoubleRight, ChevronDoubleUp, ChevronDown, ChevronLeft, ChevronRight, ChevronUp,
		ChiliHot, ChiliMedium, ChiliMild, Chip, Church, Circle, CircleOutline, CiscoWebex, City, Clipboard, ClipboardAccount, ClipboardAlert, ClipboardArrowDown, ClipboardArrowLeft, ClipboardCheck, ClipboardFlow, ClipboardOutline, ClipboardPlus,
		ClipboardText, Clippy, Clock, ClockAlert, ClockEnd, ClockFast, ClockIn, ClockOut, ClockStart, Close, CloseBox, CloseBoxOutline, CloseCircle, CloseCircleOutline, CloseNetwork, CloseOctagon, CloseOctagonOutline, CloseOutline,
		ClosedCaption, Cloud, CloudBraces, CloudCheck, CloudCircle, CloudDownload, CloudOffOutline, CloudOutline, CloudPrint, CloudPrintOutline, CloudSync, CloudTags, CloudUpload, Clover, CodeArray, CodeBraces, CodeBrackets, CodeEqual,
		CodeGreaterThan, CodeGreaterThanOrEqual, CodeLessThan, CodeLessThanOrEqual, CodeNotEqual, CodeNotEqualVariant, CodeParentheses, CodeString, CodeTags,
		CodeTagsCheck, Codepen, Coffee, CoffeeOutline, CoffeeToGo, Coin, Coins, Collage, ColorHelper, Comment, CommentAccount, CommentAccountOutline, CommentAlert, CommentAlertOutline, CommentCheck, CommentCheckOutline, CommentMultipleOutline, CommentOutline,
		CommentPlusOutline, CommentProcessing, CommentProcessingOutline, CommentQuestion, CommentQuestionOutline, CommentRemove, CommentRemoveOutline, CommentText, CommentTextOutline,
		Compare, Compass, CompassOutline, Console, ConsoleLine, ContactMail, Contacts, ContentCopy, ContentCut, ContentDuplicate, ContentPaste, ContentSave, ContentSaveAll, ContentSaveOutline, ContentSaveSettings, Contrast, ContrastBox, ContrastCircle,
		Cookie, Copyright, Corn, Counter, Cow, Creation, CreditCard, CreditCardMultiple, CreditCardOff,
		CreditCardPlus, CreditCardScan, Crop, CropFree, CropLandscape, CropPortrait, CropRotate, CropSquare, Crosshairs, CrosshairsGps, Crown, Cube, CubeOutline, CubeSend, CubeUnfolded, Cup, CupOff, CupWater,
		CurrencyBtc, CurrencyChf, CurrencyCny, CurrencyEth, CurrencyEur, CurrencyGbp, CurrencyInr, CurrencyJpy, CurrencyKrw, CurrencyNgn, CurrencyRub, CurrencySign, CurrencyTry, CurrencyTwd,
		CurrencyUsd, CurrencyUsdOff, CursorDefault, CursorDefaultOutline, CursorMove, CursorPointer, CursorText, Database, DatabaseMinus, DatabasePlus, DatabaseSearch, DebugStepInto, DebugStepOut,
		DebugStepOver, Decagram, DecagramOutline, DecimalDecrease, DecimalIncrease, Delete, DeleteCircle, DeleteEmpty, DeleteForever, DeleteRestore, DeleteSweep, DeleteVariant, Delta, Deskphone, DesktopClassic, DesktopMac, DesktopTower, Details,
		DeveloperBoard, Deviantart, Dialpad, Diamond, Dice1, Dice2, Dice3, Dice4, Dice5, Directions, DirectionsFork, Discord, Disk, DiskAlert, Disqus, DisqusOutline, Division, DivisionBox,
		Dna, Dns, DoNotDisturb, DoNotDisturbOff, Dolby, Domain, Donkey, Door, DoorClosed, DoorOpen, DotsHorizontal, DotsHorizontalCircle, DotsVertical, DotsVerticalCircle, Douban, Download, DownloadNetwork, Drag,
		DragHorizontal, DragVertical, Drawing, DrawingBox, Dribbble, DribbbleBox, Drone, Dropbox, Drupal, Duck, Dumbbell, EarHearing, Earth, EarthBox, EarthBoxOff, EarthOff, Edge, Eject,
		Elephant, ElevationDecline, ElevationRise, Elevator, Email, EmailAlert, EmailOpen, EmailOpenOutline, EmailOutline, EmailSecure, EmailVariant, Emby, Emoticon, EmoticonCool, EmoticonDead, EmoticonDevil, EmoticonExcited, EmoticonHappy,
		EmoticonNeutral, EmoticonPoop, EmoticonSad, EmoticonTongue, Engine, EngineOutline, Equal, EqualBox, Eraser, EraserVariant, Escalator, Ethernet, EthernetCable, EthernetCableOff, Etsy, EvStation, Eventbrite, Evernote,
		Exclamation, ExitToApp, Export, Eye, EyeOff, EyeOffOutline, EyeOutline, EyePlus, EyePlusOutline, Eyedropper, EyedropperVariant, Face, FaceProfile, Facebook, FacebookBox, FacebookMessenger, Factory, Fan,
		FanOff, FastForward, FastForwardOutline, Fax, Feather, Ferry, File, FileAccount, FileChart, FileCheck, FileCloud, FileDelimited, FileDocument, FileDocumentBox, FileExcel, FileExcelBox, FileExport, FileFind,
		FileHidden, FileImage, FileImport, FileLock, FileMultiple, FileMusic, FileOutline, FilePdf, FilePdfBox, FilePercent, FilePlus, FilePowerpoint, FilePowerpointBox, FilePresentationBox, FileQuestion, FileRestore, FileSend, FileTree,
		FileVideo, FileWord, FileWordBox, FileXml, Film, Filmstrip, FilmstripOff, Filter, FilterOutline, FilterRemove, FilterRemoveOutline, FilterVariant, Finance, FindReplace, Fingerprint, Fire, Firefox, Fish,
		Flag, FlagCheckered, FlagOutline, FlagTriangle, FlagVariant, FlagVariantOutline, Flash, FlashAuto, FlashCircle, FlashOff, FlashOutline, FlashRedEye, Flashlight, FlashlightOff, Flask, FlaskEmpty, FlaskEmptyOutline, FlaskOutline,
		Flattr, FlipToBack, FlipToFront, FloorPlan, Floppy, Flower, Folder, FolderAccount, FolderDownload, FolderGoogleDrive, FolderImage, FolderLock, FolderLockOpen, FolderMove, FolderMultiple, FolderMultipleImage, FolderMultipleOutline, FolderNetwork,
		FolderOpen, FolderOutline, FolderPlus, FolderRemove, FolderStar, FolderUpload, FontAwesome, Food, FoodApple, FoodCroissant, FoodForkDrink, FoodOff, FoodVariant, Football, FootballAustralian, FootballHelmet, Forklift, FormatAlignBottom,
		FormatAlignCenter, FormatAlignJustify, FormatAlignLeft, FormatAlignMiddle, FormatAlignRight, FormatAlignTop, FormatAnnotationPlus, FormatBold, FormatClear,
		FormatColorFill, FormatColorText, FormatFloatCenter, FormatFloatLeft, FormatFloatNone, FormatFloatRight, FormatFont, FormatHeader1, FormatHeader2,
		FormatHeader3, FormatHeader4, FormatHeader5, FormatHeader6, FormatHeaderDecrease, FormatHeaderEqual, FormatHeaderIncrease, FormatHeaderPound, FormatHorizontalAlignCenter,
		FormatHorizontalAlignLeft, FormatHorizontalAlignRight, FormatIndentDecrease, FormatIndentIncrease, FormatItalic, FormatLineSpacing, FormatLineStyle, FormatLineWeight, FormatListBulleted,
		FormatListBulletedType, FormatListChecks, FormatListNumbers, FormatPageBreak, FormatPaint, FormatParagraph, FormatPilcrow, FormatQuoteClose, FormatQuoteOpen,
		FormatRotate90, FormatSection, FormatSize, FormatStrikethrough, FormatStrikethroughVariant, FormatSubscript, FormatSuperscript, FormatText, FormatTextdirectionLToR,
		FormatTextdirectionRToL, FormatTitle, FormatUnderline, FormatVerticalAlignBottom, FormatVerticalAlignCenter, FormatVerticalAlignTop, FormatWrapInline, FormatWrapSquare, FormatWrapTight,
		FormatWrapTopBottom, Forum, ForumOutline, Forward, Foursquare, Fridge, FridgeFilled, FridgeFilledBottom, FridgeFilledTop,
		Fuel, Fullscreen, FullscreenExit, Function, FunctionVariant, Gamepad, GamepadVariant, Garage, GarageOpen, GasCylinder, GasStation, Gate, Gauge, Gavel, GenderFemale, GenderMale, GenderMaleFemale, GenderTransgender,
		Gesture, GestureDoubleTap, GestureSwipeDown, GestureSwipeLeft, GestureSwipeRight, GestureSwipeUp, GestureTap, GestureTwoDoubleTap, GestureTwoTap,
		Ghost, Gift, Git, GithubBox, GithubCircle, GithubFace, GlassFlute, GlassMug, GlassStange, GlassTulip, Glassdoor, Glasses, Gmail, Gnome, Golf, Gondola, Google, GoogleAnalytics,
		GoogleAssistant, GoogleCardboard, GoogleChrome, GoogleCircles, GoogleCirclesCommunities, GoogleCirclesExtended, GoogleCirclesGroup, GoogleController, GoogleControllerOff,
		GoogleDrive, GoogleEarth, GoogleGlass, GoogleHome, GoogleKeep, GoogleMaps, GoogleNearby, GooglePages, GooglePhotos,
		GooglePhysicalWeb, GooglePlay, GooglePlus, GooglePlusBox, GoogleTranslate, GoogleWallet, Gradient, GreasePencil, Grid, GridLarge, GridOff, Group, GuitarAcoustic, GuitarElectric, GuitarPick, GuitarPickOutline, GuyFawkesMask, Hackernews,
		Hamburger, HandPointingRight, Hanger, Hangouts, Harddisk, Headphones, HeadphonesBox, HeadphonesOff, HeadphonesSettings, Headset, HeadsetDock, HeadsetOff, Heart, HeartBox, HeartBoxOutline, HeartBroken, HeartHalf, HeartHalfFull,
		HeartHalfOutline, HeartOff, HeartOutline, HeartPulse, Help, HelpBox, HelpCircle, HelpCircleOutline, HelpNetwork, Hexagon, HexagonMultiple, HexagonOutline, HighDefinition, Highway, History, Hololens, Home, HomeAccount,
		HomeAssistant, HomeAutomation, HomeCircle, HomeHeart, HomeMapMarker, HomeModern, HomeOutline, HomeVariant, Hook, HookOff, Hops, Hospital, HospitalBuilding, HospitalMarker, HotTub, Hotel, Houzz, HouzzBox,
		Hulu, Human, HumanChild, HumanFemale, HumanGreeting, HumanHandsdown, HumanHandsup, HumanMale, HumanMaleFemale, HumanPregnant, HumbleBundle, IceCream, Image, ImageAlbum, ImageArea, ImageAreaClose, ImageBroken, ImageBrokenVariant,
		ImageFilter, ImageFilterBlackWhite, ImageFilterCenterFocus, ImageFilterCenterFocusWeak, ImageFilterDrama, ImageFilterFrames, ImageFilterHdr, ImageFilterNone, ImageFilterTiltShift,
		ImageFilterVintage, ImageMultiple, ImageOff, ImagePlus, Import, Inbox, InboxArrowDown, InboxArrowUp, Incognito, Infinity, Information, InformationOutline, InformationVariant, Instagram, Instapaper, InternetExplorer, InvertColors, Itunes,
		Jeepney, Jira, Jsfiddle, Json, Karate, Keg, Kettle, Key, KeyChange, KeyMinus, KeyPlus, KeyRemove, KeyVariant, Keyboard, KeyboardBackspace, KeyboardCaps, KeyboardClose, KeyboardOff,
		KeyboardReturn, KeyboardTab, KeyboardVariant, Kickstarter, Kodi, Label, LabelOutline, Ladybug, Lambda, Lamp, Lan, LanConnect, LanDisconnect, LanPending, LanguageC, LanguageCpp, LanguageCsharp, LanguageCss3,
		LanguageGo, LanguageHtml5, LanguageJavascript, LanguagePhp, LanguagePython, LanguagePythonText, LanguageR, LanguageSwift, LanguageTypescript,
		Laptop, LaptopChromebook, LaptopMac, LaptopOff, LaptopWindows, Lastfm, Lastpass, Launch, LavaLamp, Layers, LayersOff, LeadPencil, Leaf, LedOff, LedOn, LedOutline, LedStrip, LedVariantOff,
		LedVariantOn, LedVariantOutline, Library, LibraryBooks, LibraryMusic, LibraryPlus, Lightbulb, LightbulbOn, LightbulbOnOutline,
		LightbulbOutline, Link, LinkOff, LinkVariant, LinkVariantOff, Linkedin, LinkedinBox, Linux, Loading, Lock, LockOpen, LockOpenOutline, LockOutline, LockPattern, LockPlus, LockReset, Locker, LockerMultiple,
		Login, LoginVariant, Logout, LogoutVariant, Looks, Loop, Loupe, Lumx, Magnet, MagnetOn, Magnify, MagnifyMinus, MagnifyMinusOutline, MagnifyPlus, MagnifyPlusOutline, MailRu, Mailbox, Map,
		MapMarker, MapMarkerCircle, MapMarkerMinus, MapMarkerMultiple, MapMarkerOff, MapMarkerOutline, MapMarkerPlus, MapMarkerRadius, Margin,
		Markdown, Marker, MarkerCheck, Martini, MaterialUi, MathCompass, Matrix, Maxcdn, MedicalBag, Medium, Memory, Menu, MenuDown, MenuDownOutline, MenuLeft, MenuRight, MenuUp, MenuUpOutline,
		Message, MessageAlert, MessageBulleted, MessageBulletedOff, MessageDraw, MessageImage, MessageOutline, MessagePlus, MessageProcessing,
		MessageReply, MessageReplyText, MessageSettings, MessageSettingsVariant, MessageText, MessageTextOutline, MessageVideo, Meteor, Metronome,
		MetronomeTick, MicroSd, Microphone, MicrophoneOff, MicrophoneOutline, MicrophoneSettings, MicrophoneVariant, MicrophoneVariantOff, Microscope,
		Microsoft, Minecraft, Minus, MinusBox, MinusBoxOutline, MinusCircle, MinusCircleOutline, MinusNetwork, Mixcloud, MoveResize, MoveResizeVariant, Movie, MovieRoll, Multiplication, MultiplicationBox, Mushroom, MushroomOutline, Music,
		MusicBox, MusicBoxOutline, MusicCircle, MusicNote, MusicNoteBluetooth, MusicNoteBluetoothOff, MusicNoteEighth, MusicNoteHalf, MusicNoteOff,
		MusicNoteQuarter, MusicNoteSixteenth, MusicNoteWhole, MusicOff, Nature, NaturePeople, Navigation, NearMe, Needle, NestProtect, NestThermostat, Netflix, Network, NewBox, Newspaper, Nfc, NfcTap, NfcVariant,
		Ninja, NintendoSwitch, Nodejs, Note, NoteMultiple, NoteMultipleOutline, NoteOutline, NotePlus, NotePlusOutline, NoteText, Notebook, NotificationClearAll, Npm, Nuke, Null, Numeric, Numeric0Box, Numeric0BoxMultipleOutline,
		Numeric0BoxOutline, Numeric1Box, Numeric1BoxMultipleOutline, Numeric1BoxOutline, Numeric2Box, Numeric2BoxMultipleOutline, Numeric2BoxOutline, Numeric3Box, Numeric3BoxMultipleOutline,
		Numeric3BoxOutline, Numeric4Box, Numeric4BoxMultipleOutline, Numeric4BoxOutline, Numeric5Box, Numeric5BoxMultipleOutline, Numeric5BoxOutline, Numeric6Box, Numeric6BoxMultipleOutline,
		Numeric6BoxOutline, Numeric7Box, Numeric7BoxMultipleOutline, Numeric7BoxOutline, Numeric8Box, Numeric8BoxMultipleOutline, Numeric8BoxOutline, Numeric9Box, Numeric9BoxMultipleOutline,
		Numeric9BoxOutline, Numeric9PlusBox, Numeric9PlusBoxMultipleOutline, Numeric9PlusBoxOutline, Nut, Nutrition, Oar, Octagon, OctagonOutline,
		Octagram, OctagramOutline, Odnoklassniki, Office, Oil, OilTemperature, Omega, Onedrive, Onenote, Opacity, OpenInApp, OpenInNew, Openid, Opera, Orbit, Ornament, OrnamentVariant, Owl,
		Package, PackageDown, PackageUp, PackageVariant, PackageVariantClosed, PageFirst, PageLast, PageLayoutBody, PageLayoutFooter,
		PageLayoutHeader, PageLayoutSidebarLeft, PageLayoutSidebarRight, Palette, PaletteAdvanced, Panda, Pandora, Panorama, PanoramaFisheye,
		PanoramaHorizontal, PanoramaVertical, PanoramaWideAngle, PaperCutVertical, Paperclip, Parking, Passport, Pause, PauseCircle,
		PauseCircleOutline, PauseOctagon, PauseOctagonOutline, Paw, PawOff, Pen, Pencil, PencilBox, PencilBoxOutline,
		PencilCircle, PencilCircleOutline, PencilLock, PencilOff, Pentagon, PentagonOutline, Percent, PeriodicTableCo2, Periscope,
		Pharmacy, Phone, PhoneBluetooth, PhoneClassic, PhoneForward, PhoneHangup, PhoneInTalk, PhoneIncoming, PhoneLocked,
		PhoneLog, PhoneMinus, PhoneMissed, PhoneOutgoing, PhonePaused, PhonePlus, PhoneReturn, PhoneSettings, PhoneVoip, Pi, PiBox, Piano, Pig, Pill, Pillar, Pin, PinOff, PineTree,
		PineTreeBox, Pinterest, PinterestBox, Pipe, PipeDisconnected, Pistol, Pizza, PlaneShield, Play,
		PlayBoxOutline, PlayCircle, PlayCircleOutline, PlayPause, PlayProtectedContent, PlaylistCheck, PlaylistMinus, PlaylistPlay, PlaylistPlus,
		PlaylistRemove, Playstation, Plex, Plus, PlusBox, PlusBoxOutline, PlusCircle, PlusCircleMultipleOutline, PlusCircleOutline,
		PlusNetwork, PlusOne, PlusOutline, Pocket, Pokeball, PokerChip, Polaroid, Poll, PollBox, Polymer, Pool, Popcorn, Pot, PotMix, Pound, PoundBox, Power, PowerPlug,
		PowerPlugOff, PowerSettings, PowerSocket, PowerSocketEu, PowerSocketUk, PowerSocketUs, Prescription, Presentation, PresentationPlay,
		Printer, Printer3d, PrinterAlert, PrinterSettings, PriorityHigh, PriorityLow, ProfessionalHexagon, Projector, ProjectorScreen, Publish, Pulse, Puzzle, Qqchat, Qrcode, QrcodeScan, Quadcopter, QualityHigh, Quicktime,
		Radar, Radiator, Radio, RadioHandheld, RadioTower, Radioactive, RadioboxBlank, RadioboxMarked, Raspberrypi, RayEnd, RayEndArrow, RayStart, RayStartArrow, RayStartEnd, RayVertex, React, Read, Receipt,
		Record, RecordRec, Recycle, Reddit, Redo, RedoVariant, Refresh, Regex, RelativeScale, Reload, Reminder, Remote, RenameBox, ReorderHorizontal, ReorderVertical, Repeat, RepeatOff, RepeatOnce,
		Replay, Reply, ReplyAll, Reproduction, ResizeBottomRight, Responsive, Restart, Restore, Rewind, RewindOutline, Rhombus, RhombusOutline, Ribbon, Rice, Ring, Road, RoadVariant, Robot,
		Rocket, Roomba, Rotate3d, RotateLeft, RotateLeftVariant, RotateRight, RotateRightVariant, RoundedCorner, RouterWireless, Routes, Rowing, Rss, RssBox, Ruler, Run, RunFast, Sale, Sass,
		Satellite, SatelliteVariant, Saxophone, Scale, ScaleBalance, ScaleBathroom, Scanner, School, ScreenRotation,
		ScreenRotationLock, Screwdriver, Script, Sd, Seal, SearchWeb, SeatFlat, SeatFlatAngled, SeatIndividualSuite,
		SeatLegroomExtra, SeatLegroomNormal, SeatLegroomReduced, SeatReclineExtra, SeatReclineNormal, Security, SecurityAccount, SecurityHome, SecurityNetwork,
		Select, SelectAll, SelectInverse, SelectOff, Selection, SelectionOff, Send, SendSecure, SerialPort,
		Server, ServerMinus, ServerNetwork, ServerNetworkOff, ServerOff, ServerPlus, ServerRemove, ServerSecurity, SetAll,
		SetCenter, SetCenterRight, SetLeft, SetLeftCenter, SetLeftRight, SetNone, SetRight, Settings, SettingsBox,
		Shape, ShapeCirclePlus, ShapeOutline, ShapePlus, ShapePolygonPlus, ShapeRectanglePlus, ShapeSquarePlus, Share, ShareVariant,
		Shield, ShieldHalfFull, ShieldOutline, ShipWheel, Shopping, ShoppingMusic, Shovel, ShovelOff, Shredder,
		Shuffle, ShuffleDisabled, ShuffleVariant, Sigma, SigmaLower, SignCaution, SignDirection, SignText, Signal,
		Signal2g, Signal3g, Signal4g, SignalHspa, SignalHspaPlus, SignalOff, SignalVariant, Silverware, SilverwareFork,
		SilverwareSpoon, SilverwareVariant, Sim, SimAlert, SimOff, Sitemap, SkipBackward, SkipForward, SkipNext,
		SkipNextCircle, SkipNextCircleOutline, SkipPrevious, SkipPreviousCircle, SkipPreviousCircleOutline, Skull, Skype, SkypeBusiness, Slack,
		Sleep, SleepOff, Smoking, SmokingOff, Snapchat, Snowflake, Snowman, Soccer, SoccerField,
		Sofa, Solid, Sort, SortAlphabetical, SortAscending, SortDescending, SortNumeric, SortVariant, Soundcloud,
		SourceBranch, SourceCommit, SourceCommitEnd, SourceCommitEndLocal, SourceCommitLocal, SourceCommitNextLocal, SourceCommitStart, SourceCommitStartNextLocal, SourceFork,
		SourceMerge, SourcePull, SoySauce, Speaker, SpeakerOff, SpeakerWireless, Speedometer, Spellcheck, Spotify,
		Spotlight, SpotlightBeam, Spray, Square, SquareInc, SquareIncCash, SquareOutline, SquareRoot, StackOverflow,
		Stackexchange, Stadium, Stairs, StandardDefinition, Star, StarCircle, StarHalf, StarOff, StarOutline,
		Steam, Steering, StepBackward, StepBackward2, StepForward, StepForward2, Stethoscope, Sticker, StickerEmoji,
		Stocking, Stop, StopCircle, StopCircleOutline, Store, Store24Hour, Stove, SubdirectoryArrowLeft, SubdirectoryArrowRight,
		Subway, SubwayVariant, Summit, Sunglasses, SurroundSound, SurroundSound20, SurroundSound31, SurroundSound51, SurroundSound71, Svg, SwapHorizontal, SwapVertical, Swim, Switch, Sword, SwordCross, Sync, SyncAlert,
		SyncOff, Tab, TabPlus, TabUnselected, Table, TableColumn, TableColumnPlusAfter, TableColumnPlusBefore, TableColumnRemove,
		TableColumnWidth, TableEdit, TableLarge, TableOfContents, TableRow, TableRowHeight, TableRowPlusAfter, TableRowPlusBefore, TableRowRemove,
		TableSettings, Tablet, TabletAndroid, TabletIpad, Taco, Tag, TagFaces, TagHeart, TagMultiple, TagOutline, TagPlus, TagRemove, TagTextOutline, Target, Taxi, Teamviewer, Telegram, Television,
		TelevisionBox, TelevisionClassic, TelevisionClassicOff, TelevisionGuide, TelevisionOff, TemperatureCelsius, TemperatureFahrenheit, TemperatureKelvin, Tennis,
		Tent, Terrain, TestTube, TextShadow, TextToSpeech, TextToSpeechOff, Textbox, TextboxPassword, Texture,
		Theater, ThemeLightDark, Thermometer, ThermometerLines, ThermostatBox, ThoughtBubble, ThoughtBubbleOutline, ThumbDown, ThumbDownOutline,
		ThumbUp, ThumbUpOutline, ThumbsUpDown, Ticket, TicketAccount, TicketConfirmation, TicketPercent, Tie, Tilde,
		Timelapse, Timer, Timer10, Timer3, TimerOff, TimerSand, TimerSandEmpty, TimerSandFull, Timetable,
		ToggleSwitch, ToggleSwitchOff, Tooltip, TooltipEdit, TooltipImage, TooltipOutline, TooltipOutlinePlus, TooltipText, Tooth,
		Tor, TowerBeach, TowerFire, Towing, Trackpad, Tractor, TrafficLight, Train, Tram, Transcribe, TranscribeClose, Transfer, TransitTransfer, Translate, TreasureChest, Tree, Trello, TrendingDown,
		TrendingNeutral, TrendingUp, Triangle, TriangleOutline, Trophy, TrophyAward, TrophyOutline, TrophyVariant, TrophyVariantOutline,
		Truck, TruckDelivery, TruckFast, TruckTrailer, TshirtCrew, TshirtV, Tumblr, TumblrReblog, Tune,
		TuneVertical, Twitch, Twitter, TwitterBox, TwitterCircle, TwitterRetweet, Uber, Ubuntu, UltraHighDefinition,
		Umbraco, Umbrella, UmbrellaOutline, Undo, UndoVariant, UnfoldLessHorizontal, UnfoldLessVertical, UnfoldMoreHorizontal, UnfoldMoreVertical, Ungroup, Unity, Untappd, Update, Upload, UploadMultiple, UploadNetwork, Usb, VanPassenger,
		VanUtility, Vanish, VectorArrangeAbove, VectorArrangeBelow, VectorCircle, VectorCircleVariant, VectorCombine, VectorCurve, VectorDifference,
		VectorDifferenceAb, VectorDifferenceBa, VectorEllipse, VectorIntersection, VectorLine, VectorPoint, VectorPolygon, VectorPolyline, VectorRadius,
		VectorRectangle, VectorSelection, VectorSquare, VectorTriangle, VectorUnion, Venmo, Verified, Vibrate, Video,
		Video3d, Video4kBox, VideoInputAntenna, VideoInputComponent, VideoInputHdmi, VideoInputSvideo, VideoOff, VideoSwitch, ViewAgenda,
		ViewArray, ViewCarousel, ViewColumn, ViewDashboard, ViewDashboardVariant, ViewDay, ViewGrid, ViewHeadline, ViewList,
		ViewModule, ViewParallel, ViewQuilt, ViewSequential, ViewStream, ViewWeek, Vimeo, Violin, Visualstudio,
		Vk, VkBox, VkCircle, Vlc, Voice, Voicemail, VolumeHigh, VolumeLow, VolumeMedium, VolumeMinus, VolumeMute, VolumeOff, VolumePlus, Vpn, Vuejs, Walk, Wall, Wallet,
		WalletGiftcard, WalletMembership, WalletTravel, Wan, WashingMachine, Watch, WatchExport, WatchImport, WatchVibrate, Water, WaterOff, WaterPercent, WaterPump, Watermark, Waves, WeatherCloudy, WeatherFog, WeatherHail,
		WeatherLightning, WeatherLightningRainy, WeatherNight, WeatherPartlycloudy, WeatherPouring, WeatherRainy, WeatherSnowy, WeatherSnowyRainy, WeatherSunny,
		WeatherSunset, WeatherSunsetDown, WeatherSunsetUp, WeatherWindy, WeatherWindyVariant, Web, Webcam, Webhook, Webpack,
		Wechat, Weight, WeightKilogram, Whatsapp, WheelchairAccessibility, WhiteBalanceAuto, WhiteBalanceIncandescent, WhiteBalanceIridescent, WhiteBalanceSunny,
		Widgets, Wifi, WifiOff, Wii, Wiiu, Wikipedia, WindowClose, WindowClosed, WindowMaximize, WindowMinimize, WindowOpen, WindowRestore, Windows, Wordpress, Worker, Wrap, Wrench, Wunderlist,
		Xamarin, XamarinOutline, Xaml, Xbox, XboxController, XboxControllerBatteryAlert, XboxControllerBatteryEmpty, XboxControllerBatteryFull, XboxControllerBatteryLow,
		XboxControllerBatteryMedium, XboxControllerBatteryUnknown, XboxControllerOff, Xda, Xing, XingBox, XingCircle, Xml, Xmpp, Yammer, Yeast, Yelp, YinYang, YoutubeCreatorStudio, YoutubeGaming, YoutubePlay, YoutubeTv, ZipBox,
	}

	public static class Extensions
	{
		public static string ToNewString(this TimeSpan time) => time.ToString("c").Substring(3, 5);
		
		public static BitmapImage GetBitmap(this IconKind icon, Brush foreground = null)
		{
			var control = new MaterialButton()
			{
				Icon = icon,
				Foreground = foreground ?? Brushes.White,
				Background = Brushes.Transparent
			};
			control.UpdateLayout();
			control.Height = 100;
			control.Width = 100;
			PngBitmapEncoder encoder = new PngBitmapEncoder();
			encoder.Frames.Clear();
			Transform transform = control.LayoutTransform;
			control.LayoutTransform = null;
			Size size = new Size(control.Width, control.Height);
			control.Measure(size);
			control.Arrange(new Rect(size));

			RenderTargetBitmap renderBitmap =
			  new RenderTargetBitmap(
				(Int32)size.Width,
				(Int32)size.Height,
				96d,
				96d,
				PixelFormats.Pbgra32);
			renderBitmap.Render(control);

			MemoryStream memStream = new MemoryStream();

			encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
			encoder.Save(memStream);
			memStream.Flush();
			var output = new BitmapImage();
			output.BeginInit();
			output.StreamSource = memStream;
			output.EndInit();
			return output;
		}
	}

}
