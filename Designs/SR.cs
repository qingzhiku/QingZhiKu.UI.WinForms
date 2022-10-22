﻿using System.ComponentModel.Design;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace System.Design
{
    public sealed class SR
    {
		private static SR? loader;
		private ResourceManager resources;
		private static CultureInfo? Culture => null;
		public static ResourceManager Resources => GetLoader().resources;

		internal SR()
		{
            resources = new ResourceManager("QingZhiKu.UI.Designs.Resource", Assembly.GetExecutingAssembly());
        }

		private static SR GetLoader()
		{
			if (loader == null)
			{
                SR value = new SR();
				Interlocked.CompareExchange(ref loader, value, null);
			}

			return loader;
		}

		public static string GetString(string name, params object[] args)
		{
			SR sR = GetLoader();
			if (sR == null)
			{
				return String.Empty;
			}
			string @string = sR.resources.GetString(name, Culture) ?? String.Empty;
			if (args != null && args.Length != 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					if (args[i] is string text && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		public static string GetString(string name)
		{
			return GetString(name, Culture);
        }

        public static string GetString(string name, CultureInfo? culture)
        {
            return Resources?.GetString(name, culture) ?? String.Empty;
        }

        public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return GetString(name);
		}

		public static object GetObject(string name)
		{
			return Resources?.GetObject(name, Culture) ?? String.Empty;
        }




        #region const
        internal const string VerbEditorDefault = "VerbEditorDefault";

        internal const string WorkingDirectoryEditorLabel = "WorkingDirectoryEditorLabel";

        internal const string FSWPathEditorLabel = "FSWPathEditorLabel";

        internal const string BinaryEditorFileError = "BinaryEditorFileError";

        internal const string BinaryEditorTitle = "BinaryEditorTitle";

        internal const string BinaryEditorAllFiles = "BinaryEditorAllFiles";

        internal const string BinaryEditorSaveFile = "BinaryEditorSaveFile";

        internal const string BinaryEditorFileName = "BinaryEditorFileName";

        internal const string AnchorEditorAccName = "AnchorEditorAccName";

        internal const string AnchorEditorRightAccName = "AnchorEditorRightAccName";

        internal const string AnchorEditorLeftAccName = "AnchorEditorLeftAccName";

        internal const string AnchorEditorTopAccName = "AnchorEditorTopAccName";

        internal const string AnchorEditorBottomAccName = "AnchorEditorBottomAccName";

        internal const string CollectionEditorCaption = "CollectionEditorCaption";

        internal const string CollectionEditorProperties = "CollectionEditorProperties";

        internal const string CollectionEditorPropertiesMultiSelect = "CollectionEditorPropertiesMultiSelect";

        internal const string CollectionEditorPropertiesNone = "CollectionEditorPropertiesNone";

        internal const string CollectionEditorCantRemoveItem = "CollectionEditorCantRemoveItem";

        internal const string CollectionEditorUndoBatchDesc = "CollectionEditorUndoBatchDesc";

        internal const string CollectionEditorInheritedReadOnlySelection = "CollectionEditorInheritedReadOnlySelection";

        internal const string DockEditorAccName = "DockEditorAccName";

        internal const string DockEditorNoneAccName = "DockEditorNoneAccName";

        internal const string DockEditorRightAccName = "DockEditorRightAccName";

        internal const string DockEditorLeftAccName = "DockEditorLeftAccName";

        internal const string DockEditorTopAccName = "DockEditorTopAccName";

        internal const string DockEditorBottomAccName = "DockEditorBottomAccName";

        internal const string DockEditorFillAccName = "DockEditorFillAccName";

        internal const string DesignSurfaceNoRootComponent = "DesignSurfaceNoRootComponent";

        internal const string DesignSurfaceServiceIsFixed = "DesignSurfaceServiceIsFixed";

        internal const string DesignSurfaceFatalError = "DesignSurfaceFatalError";

        internal const string DesignSurfaceContainerDispose = "DesignSurfaceContainerDispose";

        internal const string DesignSurfaceDesignerNotLoaded = "DesignSurfaceDesignerNotLoaded";

        internal const string DesignSurfaceNoSupportedTechnology = "DesignSurfaceNoSupportedTechnology";

        internal const string DesignerHostUnloading = "DesignerHostUnloading";

        internal const string DesignerHostCyclicAdd = "DesignerHostCyclicAdd";

        internal const string DesignerHostNoTopLevelDesigner = "DesignerHostNoTopLevelDesigner";

        internal const string DesignerHostDuplicateName = "DesignerHostDuplicateName";

        internal const string DesignerHostFailedComponentCreate = "DesignerHostFailedComponentCreate";

        internal const string DesignerHostCantDestroyInheritedComponent = "DesignerHostCantDestroyInheritedComponent";

        internal const string DesignerHostDestroyComponentTransaction = "DesignerHostDestroyComponentTransaction";

        internal const string DesignerHostNoBaseClass = "DesignerHostNoBaseClass";

        internal const string DesignerHostLoaderSpecified = "DesignerHostLoaderSpecified";

        internal const string DesignerHostNestedTransaction = "DesignerHostNestedTransaction";

        internal const string DesignerHostGenericTransactionName = "DesignerHostGenericTransactionName";

        internal const string DesignerHostDesignerNeedsComponent = "DesignerHostDesignerNeedsComponent";

        internal const string DesignerOptionsMissingServiceContainer = "DesignerOptionsMissingServiceContainer";

        internal const string DesignerOptionsExistingOptionsService = "DesignerOptionsExistingOptionsService";

        internal const string DesignerOptionsUnableToCreateOptionService = "DesignerOptionsUnableToCreateOptionService";

        internal const string BasicDesignerLoaderAlreadyLoaded = "BasicDesignerLoaderAlreadyLoaded";

        internal const string BasicDesignerLoaderDifferentHost = "BasicDesignerLoaderDifferentHost";

        internal const string BasicDesignerLoaderMissingService = "BasicDesignerLoaderMissingService";

        internal const string BasicDesignerLoaderNotInitialized = "BasicDesignerLoaderNotInitialized";

        internal const string CatAction = "CatAction";

        internal const string CatAppearance = "CatAppearance";

        internal const string CatBehavior = "CatBehavior"; 

        internal const string CatPropertyChanged = "CatPropertyChanged"; 

        internal const string CodeDomDesignerLoaderNoLanguageSupport = "CodeDomDesignerLoaderNoLanguageSupport";

        internal const string CodeDomDesignerLoaderDocumentFailureTypeNotFound = "CodeDomDesignerLoaderDocumentFailureTypeNotFound";

        internal const string CodeDomDesignerLoaderDocumentFailureTypeNotDesignable = "CodeDomDesignerLoaderDocumentFailureTypeNotDesignable";

        internal const string CodeDomDesignerLoaderDocumentFailureTypeDesignerNotInstalled = "CodeDomDesignerLoaderDocumentFailureTypeDesignerNotInstalled";

        internal const string CodeDomDesignerLoaderNoRootSerializer = "CodeDomDesignerLoaderNoRootSerializer";

        internal const string CodeDomDesignerLoaderNoRootSerializerWithFailures = "CodeDomDesignerLoaderNoRootSerializerWithFailures";

        internal const string CodeDomDesignerLoaderInvalidIdentifier = "CodeDomDesignerLoaderInvalidIdentifier";

        internal const string CodeDomDesignerLoaderInvalidBlankIdentifier = "CodeDomDesignerLoaderInvalidBlankIdentifier";

        internal const string CodeDomDesignerLoaderDupComponentName = "CodeDomDesignerLoaderDupComponentName";

        internal const string CodeDomDesignerLoaderBadSerializationObject = "CodeDomDesignerLoaderBadSerializationObject";

        internal const string CodeDomDesignerLoaderPropModifiers = "CodeDomDesignerLoaderPropModifiers";

        internal const string CodeDomDesignerLoaderPropGenerateMember = "CodeDomDesignerLoaderPropGenerateMember";

        internal const string CodeDomDesignerLoaderNoTypeResolution = "CodeDomDesignerLoaderNoTypeResolution";

        internal const string CodeDomDesignerLoaderSerializerTypeNotFirstType = "CodeDomDesignerLoaderSerializerTypeNotFirstType";

        internal const string CodeDomComponentSerializationServiceUnknownStore = "CodeDomComponentSerializationServiceUnknownStore";

        internal const string CodeDomComponentSerializationServiceClosedStore = "CodeDomComponentSerializationServiceClosedStore";

        internal const string CodeDomComponentSerializationServiceDeserializationError = "CodeDomComponentSerializationServiceDeserializationError";

        internal const string DesignerActionPanel_CouldNotFindProperty = "DesignerActionPanel_CouldNotFindProperty";

        internal const string DesignerActionPanel_CouldNotFindMethod = "DesignerActionPanel_CouldNotFindMethod";

        internal const string DesignerActionPanel_CouldNotConvertValue = "DesignerActionPanel_CouldNotConvertValue";

        internal const string DesignerActionPanel_ErrorActivatingDropDown = "DesignerActionPanel_ErrorActivatingDropDown";

        internal const string DesignerActionPanel_ErrorSettingValue = "DesignerActionPanel_ErrorSettingValue";

        internal const string DesignerActionPanel_ErrorInvokingAction = "DesignerActionPanel_ErrorInvokingAction";

        internal const string DesignerActionPanel_DefaultPanelTitle = "DesignerActionPanel_DefaultPanelTitle";

        internal const string ExtenderProviderServiceDuplicateProvider = "ExtenderProviderServiceDuplicateProvider";

        internal const string EventBindingServiceMissingService = "EventBindingServiceMissingService";

        internal const string EventBindingServiceEventReadOnly = "EventBindingServiceEventReadOnly";

        internal const string EventBindingServiceBadArgType = "EventBindingServiceBadArgType";

        internal const string EventBindingServiceNoSite = "EventBindingServiceNoSite";

        internal const string EventBindingServiceSetValue = "EventBindingServiceSetValue";

        internal const string SerializationManagerDuplicateComponentDecl = "SerializationManagerDuplicateComponentDecl";

        internal const string SerializationManagerNoMatchingCtor = "SerializationManagerNoMatchingCtor";

        internal const string SerializationManagerNameInUse = "SerializationManagerNameInUse";

        internal const string SerializationManagerObjectHasName = "SerializationManagerObjectHasName";

        internal const string SerializationManagerAreadyInSession = "SerializationManagerAreadyInSession";

        internal const string SerializationManagerNoSession = "SerializationManagerNoSession";

        internal const string SerializationManagerWithinSession = "SerializationManagerWithinSession";

        internal const string UndoEngineMissingService = "UndoEngineMissingService";

        internal const string UndoEngineComponentChange0 = "UndoEngineComponentChange0";

        internal const string UndoEngineComponentChange1 = "UndoEngineComponentChange1";

        internal const string UndoEngineComponentChange2 = "UndoEngineComponentChange2";

        internal const string UndoEngineComponentAdd0 = "UndoEngineComponentAdd0";

        internal const string UndoEngineComponentAdd1 = "UndoEngineComponentAdd1";

        internal const string UndoEngineComponentRemove0 = "UndoEngineComponentRemove0";

        internal const string UndoEngineComponentRemove1 = "UndoEngineComponentRemove1";

        internal const string UndoEngineComponentRename = "UndoEngineComponentRename";

        internal const string BehaviorServiceResizeControl = "BehaviorServiceResizeControl";

        internal const string BehaviorServiceResizeControls = "BehaviorServiceResizeControls";

        internal const string BehaviorServiceMoveControl = "BehaviorServiceMoveControl";

        internal const string BehaviorServiceMoveControls = "BehaviorServiceMoveControls";

        internal const string BehaviorServiceCopyControl = "BehaviorServiceCopyControl";

        internal const string BehaviorServiceCopyControls = "BehaviorServiceCopyControls";

        internal const string MultilineStringEditorWatermark = "MultilineStringEditorWatermark";

        internal const string ComponentDesignerAddEvent = "ComponentDesignerAddEvent";

        internal const string LocalizerManualReload = "LocalizerManualReload";

        internal const string LocalizingCannotAdd = "LocalizingCannotAdd";

        internal const string LocalizeDesigner_RegionWatermark = "LocalizeDesigner_RegionWatermark";

        internal const string LocalizationProviderLocalizableDescr = "LocalizationProviderLocalizableDescr";

        internal const string LocalizationProviderLanguageDescr = "LocalizationProviderLanguageDescr";

        internal const string LocalizationProviderManualReload = "LocalizationProviderManualReload";

        internal const string LocalizationProviderMissingService = "LocalizationProviderMissingService";

        internal const string IntegerCollectionEditorTitle = "IntegerCollectionEditorTitle";

        internal const string InheritanceServiceReadOnlyCollection = "InheritanceServiceReadOnlyCollection";

        internal const string CancelCaption = "CancelCaption";

        internal const string OKCaption = "OKCaption";

        internal const string HelpCaption = "HelpCaption";

        internal const string DataFieldCollectionEditorTitle = "DataFieldCollectionEditorTitle";

        internal const string DataFieldCollectionAvailableFields = "DataFieldCollectionAvailableFields";

        internal const string DataFieldCollectionSelectedFields = "DataFieldCollectionSelectedFields";

        internal const string DataFieldCollection_MoveUp = "DataFieldCollection_MoveUp";

        internal const string DataFieldCollection_MoveUpDesc = "DataFieldCollection_MoveUpDesc";

        internal const string DataFieldCollection_MoveDown = "DataFieldCollection_MoveDown";

        internal const string DataFieldCollection_MoveDownDesc = "DataFieldCollection_MoveDownDesc";

        internal const string DataFieldCollection_MoveLeft = "DataFieldCollection_MoveLeft";

        internal const string DataFieldCollection_MoveLeftDesc = "DataFieldCollection_MoveLeftDesc";

        internal const string DataFieldCollection_MoveRight = "DataFieldCollection_MoveRight";

        internal const string DataFieldCollection_MoveRightDesc = "DataFieldCollection_MoveRightDesc";

        internal const string SerializerBadElementType = "SerializerBadElementType";

        internal const string SerializerBadElementTypes = "SerializerBadElementTypes";

        internal const string SerializerMissingService = "SerializerMissingService";

        internal const string SerializerNoSerializerForComponent = "SerializerNoSerializerForComponent";

        internal const string SerializerLostStatements = "SerializerLostStatements";

        internal const string SerializerTypeNotFound = "SerializerTypeNotFound";

        internal const string SerializerTypeAbstract = "SerializerTypeAbstract";

        internal const string SerializerUndeclaredName = "SerializerUndeclaredName";

        internal const string SerializerNoSuchEvent = "SerializerNoSuchEvent";

        internal const string SerializerNoSuchField = "SerializerNoSuchField";

        internal const string SerializerNoSuchProperty = "SerializerNoSuchProperty";

        internal const string SerializerNullNestedProperty = "SerializerNullNestedProperty";

        internal const string SerializerInvalidArrayRank = "SerializerInvalidArrayRank";

        internal const string SerializerResourceException = "SerializerResourceException";

        internal const string SerializerResourceExceptionInvariant = "SerializerResourceExceptionInvariant";

        internal const string SerializerPropertyGenFailed = "SerializerPropertyGenFailed";

        internal const string SerializerFieldTargetEvalFailed = "SerializerFieldTargetEvalFailed";

        internal const string SerializerMemberTypeNotSerializable = "SerializerMemberTypeNotSerializable";

        internal const string SerializerNoRootExpression = "SerializerNoRootExpression";

        internal const string AXAbout = "AXAbout";

        internal const string AXCannotLoadTypeLib = "AXCannotLoadTypeLib";

        internal const string AXCannotOverwriteFile = "AXCannotOverwriteFile";

        internal const string AXReadOnlyFile = "AXReadOnlyFile";

        internal const string AXCompilerError = "AXCompilerError";

        internal const string Ax_Control = "Ax_Control";

        internal const string AXEdit = "AXEdit";

        internal const string AxImportFailed = "AxImportFailed";

        internal const string AXNoActiveXControls = "AXNoActiveXControls";

        internal const string AXNotRegistered = "AXNotRegistered";

        internal const string AXNotValidControl = "AXNotValidControl";

        internal const string AxImpNoDefaultValue = "AxImpNoDefaultValue";

        internal const string AxImpUnrecognizedDefaultValueType = "AxImpUnrecognizedDefaultValueType";

        internal const string AXProperties = "AXProperties";

        internal const string AXVerbPrefix = "AXVerbPrefix";

        internal const string AdvancedBindingPropertyDescriptorDesc = "AdvancedBindingPropertyDescriptorDesc";

        internal const string AdvancedBindingPropertyDescName = "AdvancedBindingPropertyDescName";

        internal const string AutoAdjustMargins = "AutoAdjustMargins";

        internal const string BaseNodeName = "BaseNodeName";

        internal const string BindingFormattingDialogAllTreeNode = "BindingFormattingDialogAllTreeNode";

        internal const string BindingFormattingDialogBindingPickerAccName = "BindingFormattingDialogBindingPickerAccName";

        internal const string BindingFormattingDialogCommonTreeNode = "BindingFormattingDialogCommonTreeNode";

        internal const string BindingFormattingDialogCustomFormat = "BindingFormattingDialogCustomFormat";

        internal const string BindingFormattingDialogCustomFormatAccessibleDescription = "BindingFormattingDialogCustomFormatAccessibleDescription";

        internal const string BindingFormattingDialogDataSourcePickerDropDownAccName = "BindingFormattingDialogDataSourcePickerDropDownAccName";

        internal const string BindingFormattingDialogDecimalPlaces = "BindingFormattingDialogDecimalPlaces";

        internal const string BindingFormattingDialogFormatTypeCurrency = "BindingFormattingDialogFormatTypeCurrency";

        internal const string BindingFormattingDialogFormatTypeCurrencyExplanation = "BindingFormattingDialogFormatTypeCurrencyExplanation";

        internal const string BindingFormattingDialogFormatTypeCustom = "BindingFormattingDialogFormatTypeCustom";

        internal const string BindingFormattingDialogFormatTypeCustomExplanation = "BindingFormattingDialogFormatTypeCustomExplanation";

        internal const string BindingFormattingDialogFormatTypeCustomInvalidFormat = "BindingFormattingDialogFormatTypeCustomInvalidFormat";

        internal const string BindingFormattingDialogFormatTypeDateTime = "BindingFormattingDialogFormatTypeDateTime";

        internal const string BindingFormattingDialogFormatTypeDateTimeExplanation = "BindingFormattingDialogFormatTypeDateTimeExplanation";

        internal const string BindingFormattingDialogFormatTypeNoFormatting = "BindingFormattingDialogFormatTypeNoFormatting";

        internal const string BindingFormattingDialogFormatTypeNoFormattingExplanation = "BindingFormattingDialogFormatTypeNoFormattingExplanation";

        internal const string BindingFormattingDialogFormatTypeNumeric = "BindingFormattingDialogFormatTypeNumeric";

        internal const string BindingFormattingDialogFormatTypeNumericExplanation = "BindingFormattingDialogFormatTypeNumericExplanation";

        internal const string BindingFormattingDialogFormatTypeScientific = "BindingFormattingDialogFormatTypeScientific";

        internal const string BindingFormattingDialogFormatTypeScientificExplanation = "BindingFormattingDialogFormatTypeScientificExplanation";

        internal const string BindingFormattingDialogList = "BindingFormattingDialogList";

        internal const string BindingFormattingDialogNullValue = "BindingFormattingDialogNullValue";

        internal const string BindingFormattingDialogType = "BindingFormattingDialogType";

        internal const string CellStyleBuilderPreview = "CellStyleBuilderPreview";

        internal const string CellStyleBuilderPreviewText = "CellStyleBuilderPreviewText";

        internal const string CellStyleBuilderTitle = "CellStyleBuilderTitle";

        internal const string CellStyleBuilderNormalPreviewAccName = "CellStyleBuilderNormalPreviewAccName";

        internal const string CellStyleBuilderSelectedPreviewAccName = "CellStyleBuilderSelectedPreviewAccName";

        internal const string CommandSetAlignByPrimary = "CommandSetAlignByPrimary";

        internal const string CommandSetAlignToGrid = "CommandSetAlignToGrid";

        internal const string CommandSetBringToFront = "CommandSetBringToFront";

        internal const string CommandSetCutMultiple = "CommandSetCutMultiple";

        internal const string CommandSetDelete = "CommandSetDelete";

        internal const string CommandSetError = "CommandSetError";

        internal const string CommandSetFormatSpacing = "CommandSetFormatSpacing";

        internal const string CommandSetLockControls = "CommandSetLockControls";

        internal const string CommandSetPaste = "CommandSetPaste";

        internal const string CommandSetSendToBack = "CommandSetSendToBack";

        internal const string CommandSetSize = "CommandSetSize";

        internal const string CommandSetSizeToGrid = "CommandSetSizeToGrid";

        internal const string CommandSetUnknownSpacingCommand = "CommandSetUnknownSpacingCommand";

        internal const string CompositionDesignerWaterMark = "CompositionDesignerWaterMark";

        internal const string CompositionDesignerWaterMarkFirstLink = "CompositionDesignerWaterMarkFirstLink";

        internal const string CompositionDesignerWaterMarkSecondLink = "CompositionDesignerWaterMarkSecondLink";

        internal const string DataGridAdvancedBindingString = "DataGridAdvancedBindingString";

        internal const string DataGridNoneString = "DataGridNoneString";

        internal const string DataGridPopulateError = "DataGridPopulateError";

        internal const string DataGridAutoFormatString = "DataGridAutoFormatString";

        internal const string DataGridAutoFormatUndoTitle = "DataGridAutoFormatUndoTitle";

        internal const string DataGridAutoFormatSchemeName256Color1 = "DataGridAutoFormatSchemeName256Color1";

        internal const string DataGridAutoFormatSchemeName256Color2 = "DataGridAutoFormatSchemeName256Color2";

        internal const string DataGridAutoFormatSchemeNameClassic = "DataGridAutoFormatSchemeNameClassic";

        internal const string DataGridAutoFormatSchemeNameColorful1 = "DataGridAutoFormatSchemeNameColorful1";

        internal const string DataGridAutoFormatSchemeNameColorful2 = "DataGridAutoFormatSchemeNameColorful2";

        internal const string DataGridAutoFormatSchemeNameColorful3 = "DataGridAutoFormatSchemeNameColorful3";

        internal const string DataGridAutoFormatSchemeNameColorful4 = "DataGridAutoFormatSchemeNameColorful4";

        internal const string DataGridAutoFormatSchemeNameDefault = "DataGridAutoFormatSchemeNameDefault";

        internal const string DataGridAutoFormatSchemeNameProfessional1 = "DataGridAutoFormatSchemeNameProfessional1";

        internal const string DataGridAutoFormatSchemeNameProfessional2 = "DataGridAutoFormatSchemeNameProfessional2";

        internal const string DataGridAutoFormatSchemeNameProfessional3 = "DataGridAutoFormatSchemeNameProfessional3";

        internal const string DataGridAutoFormatSchemeNameProfessional4 = "DataGridAutoFormatSchemeNameProfessional4";

        internal const string DataGridAutoFormatSchemeNameSimple = "DataGridAutoFormatSchemeNameSimple";

        internal const string DataGridAutoFormatTableFirstColumn = "DataGridAutoFormatTableFirstColumn";

        internal const string DataGridAutoFormatTableSecondColumn = "DataGridAutoFormatTableSecondColumn";

        internal const string DataGridShowAllString = "DataGridShowAllString";

        internal const string DataSourceLocksItems = "DataSourceLocksItems";

        internal const string DesignBindingBadParseString = "DesignBindingBadParseString";

        internal const string DesignBindingContextRequiredWhenParsing = "DesignBindingContextRequiredWhenParsing";

        internal const string DesignBindingComponentNotFound = "DesignBindingComponentNotFound";

        internal const string DesignBindingPickerAccessibleName = "DesignBindingPickerAccessibleName";

        internal const string DesignBindingPickerAddProjDataSourceLabel = "DesignBindingPickerAddProjDataSourceLabel";

        internal const string DesignBindingPickerHelpGenAddDataSrc = "DesignBindingPickerHelpGenAddDataSrc";

        internal const string DesignBindingPickerHelpGenCurrentBinding = "DesignBindingPickerHelpGenCurrentBinding";

        internal const string DesignBindingPickerHelpGenPickBindSrc = "DesignBindingPickerHelpGenPickBindSrc";

        internal const string DesignBindingPickerHelpGenPickDataSrc = "DesignBindingPickerHelpGenPickDataSrc";

        internal const string DesignBindingPickerHelpGenPickMember = "DesignBindingPickerHelpGenPickMember";

        internal const string DesignBindingPickerHelpNodeBindSrcDM1 = "DesignBindingPickerHelpNodeBindSrcDM1";

        internal const string DesignBindingPickerHelpNodeBindSrcDS0 = "DesignBindingPickerHelpNodeBindSrcDS0";

        internal const string DesignBindingPickerHelpNodeBindSrcDS1 = "DesignBindingPickerHelpNodeBindSrcDS1";

        internal const string DesignBindingPickerHelpNodeBindSrcLM1 = "DesignBindingPickerHelpNodeBindSrcLM1";

        internal const string DesignBindingPickerHelpNodeFormInstDM1 = "DesignBindingPickerHelpNodeFormInstDM1";

        internal const string DesignBindingPickerHelpNodeFormInstDS0 = "DesignBindingPickerHelpNodeFormInstDS0";

        internal const string DesignBindingPickerHelpNodeFormInstDS1 = "DesignBindingPickerHelpNodeFormInstDS1";

        internal const string DesignBindingPickerHelpNodeFormInstLM0 = "DesignBindingPickerHelpNodeFormInstLM0";

        internal const string DesignBindingPickerHelpNodeFormInstLM1 = "DesignBindingPickerHelpNodeFormInstLM1";

        internal const string DesignBindingPickerHelpNodeInstances = "DesignBindingPickerHelpNodeInstances";

        internal const string DesignBindingPickerHelpNodeNone = "DesignBindingPickerHelpNodeNone";

        internal const string DesignBindingPickerHelpNodeOther = "DesignBindingPickerHelpNodeOther";

        internal const string DesignBindingPickerHelpNodeProject = "DesignBindingPickerHelpNodeProject";

        internal const string DesignBindingPickerHelpNodeProjectDM1 = "DesignBindingPickerHelpNodeProjectDM1";

        internal const string DesignBindingPickerHelpNodeProjectDS0 = "DesignBindingPickerHelpNodeProjectDS0";

        internal const string DesignBindingPickerHelpNodeProjectDS1 = "DesignBindingPickerHelpNodeProjectDS1";

        internal const string DesignBindingPickerHelpNodeProjectLM0 = "DesignBindingPickerHelpNodeProjectLM0";

        internal const string DesignBindingPickerHelpNodeProjectLM1 = "DesignBindingPickerHelpNodeProjectLM1";

        internal const string DesignBindingPickerHelpNodeProjectGroup = "DesignBindingPickerHelpNodeProjectGroup";

        internal const string DesignBindingPickerNodeNone = "DesignBindingPickerNodeNone";

        internal const string DesignBindingPickerNodeOther = "DesignBindingPickerNodeOther";

        internal const string DesignBindingPickerNodeProject = "DesignBindingPickerNodeProject";

        internal const string DesignBindingPickerNodeInstances = "DesignBindingPickerNodeInstances";

        internal const string DesignBindingPickerTreeViewAccessibleName = "DesignBindingPickerTreeViewAccessibleName";

        internal const string DesignerBatchCreateTool = "DesignerBatchCreateTool";

        internal const string DesignerCantParentType = "DesignerCantParentType";

        internal const string DesignerDefaultTab = "DesignerDefaultTab";

        internal const string UserControlTab = "UserControlTab";

        internal const string DesignerShortcutDockInParent = "DesignerShortcutDockInParent";

        internal const string DesignerShortcutUndockInParent = "DesignerShortcutUndockInParent";

        internal const string DesignerShortcutDockInForm = "DesignerShortcutDockInForm";

        internal const string DesignerShortcutDockInUserControl = "DesignerShortcutDockInUserControl";

        internal const string DesignerShortcutReparentControls = "DesignerShortcutReparentControls";

        internal const string DesignerShortcutHorizontalOrientation = "DesignerShortcutHorizontalOrientation";

        internal const string DesignerShortcutVerticalOrientation = "DesignerShortcutVerticalOrientation";

        internal const string DesignerNoUserControl = "DesignerNoUserControl";

        internal const string DesignerPropName = "DesignerPropName";

        internal const string DesignerBeginDragNotCalled = "DesignerBeginDragNotCalled";

        internal const string DesignerInheritedReadOnly = "DesignerInheritedReadOnly";

        internal const string DesignerInherited = "DesignerInherited";

        internal const string DesignerPropNotFound = "DesignerPropNotFound";

        internal const string TypeNotFoundInTargetFramework = "TypeNotFoundInTargetFramework";

        internal const string DragDropDragComponents = "DragDropDragComponents";

        internal const string DragDropMoveComponent = "DragDropMoveComponent";

        internal const string DragDropMoveComponents = "DragDropMoveComponents";

        internal const string DragDropSizeComponent = "DragDropSizeComponent";

        internal const string DragDropSizeComponents = "DragDropSizeComponents";

        internal const string DragDropDropComponents = "DragDropDropComponents";

        internal const string DragDropSetDataError = "DragDropSetDataError";

        internal const string GenericFileFilter = "GenericFileFilter";

        internal const string GenericOpenFile = "GenericOpenFile";

        internal const string DataGridViewAdd = "DataGridViewAdd";

        internal const string DataGridViewAddColumn = "DataGridViewAddColumn";

        internal const string DataGridViewAddColumnDialogTitle = "DataGridViewAddColumnDialogTitle";

        internal const string DataGridViewAddColumnTransactionString = "DataGridViewAddColumnTransactionString";

        internal const string DataGridViewAddColumnVerb = "DataGridViewAddColumnVerb";

        internal const string DataGridViewBoundColumnProperties = "DataGridViewBoundColumnProperties";

        internal const string DataGridViewChooseDataSource = "DataGridViewChooseDataSource";

        internal const string DataGridViewColumnTypePropertyDescription = "DataGridViewColumnTypePropertyDescription";

        internal const string DataGridViewColumnCollectionTransaction = "DataGridViewColumnCollectionTransaction";

        internal const string DataGridViewDataSourceNoLongerValid = "DataGridViewDataSourceNoLongerValid";

        internal const string DataGridViewDeleteAccName = "DataGridViewDeleteAccName";

        internal const string DataGridViewDuplicateColumnName = "DataGridViewDuplicateColumnName";

        internal const string DataGridViewChooseDataSourceTransactionString = "DataGridViewChooseDataSourceTransactionString";

        internal const string DataGridViewDisableAddingTransactionString = "DataGridViewDisableAddingTransactionString";

        internal const string DataGridViewDisableColumnReorderingTransactionString = "DataGridViewDisableColumnReorderingTransactionString";

        internal const string DataGridViewDisableDeletingTransactionString = "DataGridViewDisableDeletingTransactionString";

        internal const string DataGridViewDisableEditingTransactionString = "DataGridViewDisableEditingTransactionString";

        internal const string DataGridViewEditColumnsTransactionString = "DataGridViewEditColumnsTransactionString";

        internal const string DataGridViewEnableAdding = "DataGridViewEnableAdding";

        internal const string DataGridViewEnableAddingTransactionString = "DataGridViewEnableAddingTransactionString";

        internal const string DataGridViewEnableDeleting = "DataGridViewEnableDeleting";

        internal const string DataGridViewEnableDeletingTransactionString = "DataGridViewEnableDeletingTransactionString";

        internal const string DataGridViewEnableEditing = "DataGridViewEnableEditing";

        internal const string DataGridViewEnableEditingTransactionString = "DataGridViewEnableEditingTransactionString";

        internal const string DataGridViewEditingTransactionString = "DataGridViewEditingTransactionString";

        internal const string DataGridViewEnableColumnReordering = "DataGridViewEnableColumnReordering";

        internal const string DataGridViewEnableColumnReorderingTransactionString = "DataGridViewEnableColumnReorderingTransactionString";

        internal const string DataGridView_Cancel = "DataGridView_Cancel";

        internal const string DataGridView_Close = "DataGridView_Close";

        internal const string DataGridViewEditColumnsVerb = "DataGridViewEditColumnsVerb";

        internal const string DataGridViewEditColumns = "DataGridViewEditColumns";

        internal const string DataGridViewFrozen = "DataGridViewFrozen";

        internal const string DataGridViewDataBoundColumn = "DataGridViewDataBoundColumn";

        internal const string DataGridViewDataSourceColumns = "DataGridViewDataSourceColumns";

        internal const string DataGridViewHeaderText = "DataGridViewHeaderText";

        internal const string DataGridViewHelp = "DataGridViewHelp";

        internal const string DataGridViewMoveDownAccName = "DataGridViewMoveDownAccName";

        internal const string DataGridViewMoveUpAccName = "DataGridViewMoveUpAccName";

        internal const string DataGridViewName = "DataGridViewName";

        internal const string DataGridViewNormalLabel = "DataGridViewNormalLabel";

        internal const string DataGridView_OK = "DataGridView_OK";

        internal const string DataGridViewProperties = "DataGridViewProperties";

        internal const string DataGridViewReadOnly = "DataGridViewReadOnly";

        internal const string DataGridViewSelectedColumns = "DataGridViewSelectedColumns";

        internal const string DataGridViewSelectedLabel = "DataGridViewSelectedLabel";

        internal const string DataGridViewType = "DataGridViewType";

        internal const string DataGridViewUnboundColumn = "DataGridViewUnboundColumn";

        internal const string DataGridViewUnboundColumnProperties = "DataGridViewUnboundColumnProperties";

        internal const string DataGridViewVisible = "DataGridViewVisible";

        internal const string FailedToCreateComponent = "FailedToCreateComponent";

        internal const string FormatStringDialogTitle = "FormatStringDialogTitle";

        internal const string HelpProviderEditorFilter = "HelpProviderEditorFilter";

        internal const string HelpProviderEditorTitle = "HelpProviderEditorTitle";

        internal const string imageFileDescription = "imageFileDescription";

        internal const string ImageListDesignerBadImageListImage = "ImageListDesignerBadImageListImage";

        internal const string ImageCollectionEditorFormText = "ImageCollectionEditorFormText";

        internal const string IntegerCollectionEditorCancelCaption = "IntegerCollectionEditorCancelCaption";

        internal const string IntegerCollectionEditorInstruction = "IntegerCollectionEditorInstruction";

        internal const string IntegerCollectionEditorOKCaption = "IntegerCollectionEditorOKCaption";

        internal const string IntegerCollectionEditorHelpCaption = "IntegerCollectionEditorHelpCaption";

        internal const string InvalidArgument = "InvalidArgument";

        internal const string InvalidArgumentType = "InvalidArgumentType";

        internal const string InvalidBoundArgument = "InvalidBoundArgument";

        internal const string LinkAreaEditorCancel = "LinkAreaEditorCancel";

        internal const string LinkAreaEditorCaption = "LinkAreaEditorCaption";

        internal const string LinkAreaEditorDescription = "LinkAreaEditorDescription";

        internal const string LinkAreaEditorOK = "LinkAreaEditorOK";

        internal const string ListViewItemBaseName = "ListViewItemBaseName";

        internal const string ListViewSubItemBaseName = "ListViewSubItemBaseName";

        internal const string MaskDescriptorNullOrEmptyRequiredProperty = "MaskDescriptorNullOrEmptyRequiredProperty";

        internal const string MaskDescriptorNull = "MaskDescriptorNull";

        internal const string MaskDescriptorNotMaskFullErrorMsg = "MaskDescriptorNotMaskFullErrorMsg";

        internal const string MaskDescriptorValidatingTypeNone = "MaskDescriptorValidatingTypeNone";

        internal const string MaskDesignerDialogCustomEntry = "MaskDesignerDialogCustomEntry";

        internal const string MaskDesignerDialogDataFormat = "MaskDesignerDialogDataFormat";

        internal const string MaskDesignerDialogDlgCaption = "MaskDesignerDialogDlgCaption";

        internal const string MaskDesignerDialogMaskDescription = "MaskDesignerDialogMaskDescription";

        internal const string MaskDesignerDialogValidatingType = "MaskDesignerDialogValidatingType";

        internal const string MaskedTextBoxDesignerVerbsSetMaskDesc = "MaskedTextBoxDesignerVerbsSetMaskDesc";

        internal const string MaskedTextBoxTextEditorErrorFormatString = "MaskedTextBoxTextEditorErrorFormatString";

        internal const string MaskedTextBoxHintAsciiCharacterExpected = "MaskedTextBoxHintAsciiCharacterExpected";

        internal const string MaskedTextBoxHintAlphanumericCharacterExpected = "MaskedTextBoxHintAlphanumericCharacterExpected";

        internal const string MaskedTextBoxHintDigitExpected = "MaskedTextBoxHintDigitExpected";

        internal const string MaskedTextBoxHintSignedDigitExpected = "MaskedTextBoxHintSignedDigitExpected";

        internal const string MaskedTextBoxHintLetterExpected = "MaskedTextBoxHintLetterExpected";

        internal const string MaskedTextBoxHintPromptCharNotAllowed = "MaskedTextBoxHintPromptCharNotAllowed";

        internal const string MaskedTextBoxHintUnavailableEditPosition = "MaskedTextBoxHintUnavailableEditPosition";

        internal const string MaskedTextBoxHintNonEditPosition = "MaskedTextBoxHintNonEditPosition";

        internal const string MaskedTextBoxHintPositionOutOfRange = "MaskedTextBoxHintPositionOutOfRange";

        internal const string MaskedTextBoxHintInvalidInput = "MaskedTextBoxHintInvalidInput";

        internal const string MenuCommandService_DuplicateCommand = "MenuCommandService_DuplicateCommand";

        internal const string lockedDescr = "lockedDescr";

        internal const string ParentControlDesignerDrawGridDescr = "ParentControlDesignerDrawGridDescr";

        internal const string ParentControlDesignerSnapToGridDescr = "ParentControlDesignerSnapToGridDescr";

        internal const string ParentControlDesignerGridSizeDescr = "ParentControlDesignerGridSizeDescr";

        internal const string ParentControlDesignerLanguageDescr = "ParentControlDesignerLanguageDescr";

        internal const string ParentControlDesignerLassoShortcutRedo = "ParentControlDesignerLassoShortcutRedo";

        internal const string PerformAutoAnchor = "PerformAutoAnchor";

        internal const string RtfFileFilter = "RtfFileFilter";

        internal const string RtfOpenFile = "RtfOpenFile";

        internal const string SelectedPathEditorLabel = "SelectedPathEditorLabel";

        internal const string ShortcutKeys_InvalidKey = "ShortcutKeys_InvalidKey";

        internal const string SoundPathWavFile = "SoundPathWavFile";

        internal const string SoundPathEditorOpenFile = "SoundPathEditorOpenFile";

        internal const string SoundPlayNowString = "SoundPlayNowString";

        internal const string SplitContainerReplaceString = "SplitContainerReplaceString";

        internal const string SplitContainerReplaceCaption = "SplitContainerReplaceCaption";

        internal const string SplitterHorizontalOrientation = "SplitterHorizontalOrientation";

        internal const string SplitterVerticalOrientation = "SplitterVerticalOrientation";

        internal const string TabControlAdd = "TabControlAdd";

        internal const string TabControlAddTab = "TabControlAddTab";

        internal const string TabControlRemoveTab = "TabControlRemoveTab";

        internal const string TabControlRemove = "TabControlRemove";

        internal const string TabControlInvalidTabPageType = "TabControlInvalidTabPageType";

        internal const string TableLayoutPanelFullDesc = "TableLayoutPanelFullDesc";

        internal const string TableLayoutPanelSpanDesc = "TableLayoutPanelSpanDesc";

        internal const string TableLayoutPanelRowColResize = "TableLayoutPanelRowColResize";

        internal const string TableLayoutPanelDesignerChangeSizeTypeUndoUnit = "TableLayoutPanelDesignerChangeSizeTypeUndoUnit";

        internal const string TableLayoutPanelDesignerClearAnchor = "TableLayoutPanelDesignerClearAnchor";

        internal const string TableLayoutPanelDesignerClearDock = "TableLayoutPanelDesignerClearDock";

        internal const string TableLayoutPanelDesignerAddColumn = "TableLayoutPanelDesignerAddColumn";

        internal const string TableLayoutPanelDesignerAddRow = "TableLayoutPanelDesignerAddRow";

        internal const string TableLayoutPanelDesignerRemoveColumn = "TableLayoutPanelDesignerRemoveColumn";

        internal const string TableLayoutPanelDesignerRemoveRow = "TableLayoutPanelDesignerRemoveRow";

        internal const string TableLayoutPanelDesignerEditRowAndCol = "TableLayoutPanelDesignerEditRowAndCol";

        internal const string TableLayoutPanelDesignerRowMenu = "TableLayoutPanelDesignerRowMenu";

        internal const string TableLayoutPanelDesignerColMenu = "TableLayoutPanelDesignerColMenu";

        internal const string TableLayoutPanelDesignerAddMenu = "TableLayoutPanelDesignerAddMenu";

        internal const string TableLayoutPanelDesignerInsertMenu = "TableLayoutPanelDesignerInsertMenu";

        internal const string TableLayoutPanelDesignerDeleteMenu = "TableLayoutPanelDesignerDeleteMenu";

        internal const string TableLayoutPanelDesignerLabelMenu = "TableLayoutPanelDesignerLabelMenu";

        internal const string TableLayoutPanelDesignerDontBoldLabel = "TableLayoutPanelDesignerDontBoldLabel";

        internal const string TableLayoutPanelDesignerAbsoluteMenu = "TableLayoutPanelDesignerAbsoluteMenu";

        internal const string TableLayoutPanelDesignerPercentageMenu = "TableLayoutPanelDesignerPercentageMenu";

        internal const string TableLayoutPanelDesignerAutoSizeMenu = "TableLayoutPanelDesignerAutoSizeMenu";

        internal const string TableLayoutPanelDesignerContextMenuCut = "TableLayoutPanelDesignerContextMenuCut";

        internal const string TableLayoutPanelDesignerContextMenuCopy = "TableLayoutPanelDesignerContextMenuCopy";

        internal const string TableLayoutPanelDesignerContextMenuDelete = "TableLayoutPanelDesignerContextMenuDelete";

        internal const string TableLayoutPanelDesignerAddColumnUndoUnit = "TableLayoutPanelDesignerAddColumnUndoUnit";

        internal const string TableLayoutPanelDesignerAddRowUndoUnit = "TableLayoutPanelDesignerAddRowUndoUnit";

        internal const string TableLayoutPanelDesignerRemoveColumnUndoUnit = "TableLayoutPanelDesignerRemoveColumnUndoUnit";

        internal const string TableLayoutPanelDesignerRemoveRowUndoUnit = "TableLayoutPanelDesignerRemoveRowUndoUnit";

        internal const string TableLayoutPanelDesignerControlsSwapped = "TableLayoutPanelDesignerControlsSwapped";

        internal const string TableLayoutPanelDesignerInvalidColumnRowCount = "TableLayoutPanelDesignerInvalidColumnRowCount";

        internal const string ToolStripTemplateNodeImageResetCaption = "ToolStripTemplateNodeImageResetCaption";

        internal const string ToolStripTemplateNodeImageResetString = "ToolStripTemplateNodeImageResetString";

        internal const string ToolStripItemPropertyChangeTransaction = "ToolStripItemPropertyChangeTransaction";

        internal const string ToolStripInsertItemsVerb = "ToolStripInsertItemsVerb";

        internal const string ToolStripSelectAllVerb = "ToolStripSelectAllVerb";

        internal const string ToolStripDropDownDesignerDropDownMenu = "ToolStripDropDownDesignerDropDownMenu";

        internal const string ToolStripMorphingItemTransaction = "ToolStripMorphingItemTransaction";

        internal const string ToolStripCreatingNewItemTransaction = "ToolStripCreatingNewItemTransaction";

        internal const string ToolStripInsertingIntoDropDownTransaction = "ToolStripInsertingIntoDropDownTransaction";

        internal const string ToolStripAllowItemReorderAndAllowDropCannotBeSetToTrue = "ToolStripAllowItemReorderAndAllowDropCannotBeSetToTrue";

        internal const string ToolStripSelectMenuItem = "ToolStripSelectMenuItem";

        internal const string ToolStripPanelGlyphUnsupportedDock = "ToolStripPanelGlyphUnsupportedDock";

        internal const string WindowsFormsAddEvent = "WindowsFormsAddEvent";

        internal const string WindowsFormsCommandCenterX = "WindowsFormsCommandCenterX";

        internal const string WindowsFormsCommandCenterY = "WindowsFormsCommandCenterY";

        internal const string WindowsFormsTabOrderReadOnly = "WindowsFormsTabOrderReadOnly";

        internal const string OK = "OK";

        internal const string Cancel = "Cancel";

        internal const string Value = "Value";

        internal const string None = "None";

        internal const string Default = "Default";

        internal const string Custom = "Custom";

        internal const string Edit = "Edit";

        internal const string None_lc = "None_lc";

        internal const string Control_ErrorRendering = "Control_ErrorRendering";

        internal const string Control_ErrorRenderingShort = "Control_ErrorRenderingShort";

        internal const string Control_Expressions = "Control_Expressions";

        internal const string Control_CanOnlyBePlacedInside = "Control_CanOnlyBePlacedInside";

        internal const string ControlDesigner_DesignTimeHtmlError = "ControlDesigner_DesignTimeHtmlError";

        internal const string ControlDesigner_UnhandledException = "ControlDesigner_UnhandledException";

        internal const string ControlDesigner_TransactedChangeRequiresServiceProvider = "ControlDesigner_TransactedChangeRequiresServiceProvider";

        internal const string ControlDesigner_CouldNotGetExpressionBuilder = "ControlDesigner_CouldNotGetExpressionBuilder";

        internal const string ControlDesigner_CouldNotGetDesignTimeResourceProviderFactory = "ControlDesigner_CouldNotGetDesignTimeResourceProviderFactory";

        internal const string ControlDesigner_ArgumentMustBeOfType = "ControlDesigner_ArgumentMustBeOfType";

        internal const string ControlDesigner_EditDataBindingsRequiresID = "ControlDesigner_EditDataBindingsRequiresID";

        internal const string UnsettableComboBox_NotSet = "UnsettableComboBox_NotSet";

        internal const string ControlLocalizer_RequiresFilterService = "ControlLocalizer_RequiresFilterService";

        internal const string Wizard_NextButton = "Wizard_NextButton";

        internal const string Wizard_PreviousButton = "Wizard_PreviousButton";

        internal const string Wizard_CancelButton = "Wizard_CancelButton";

        internal const string Wizard_FinishButton = "Wizard_FinishButton";

        internal const string WizardAFmt_Scheme_Default = "WizardAFmt_Scheme_Default";

        internal const string WizardAFmt_Scheme_Classic = "WizardAFmt_Scheme_Classic";

        internal const string WizardAFmt_Scheme_Simple = "WizardAFmt_Scheme_Simple";

        internal const string WizardAFmt_Scheme_Professional = "WizardAFmt_Scheme_Professional";

        internal const string WizardAFmt_Scheme_Colorful = "WizardAFmt_Scheme_Colorful";

        internal const string Wizard_StepsView = "Wizard_StepsView";

        internal const string Wizard_StepsViewDescription = "Wizard_StepsViewDescription";

        internal const string CreateUserWizard_ConvertToCustomNavigationTemplate = "CreateUserWizard_ConvertToCustomNavigationTemplate";

        internal const string CreateUserWizard_CustomizeCreateUserStep = "CreateUserWizard_CustomizeCreateUserStep";

        internal const string CreateUserWizard_CustomizeCreateUserStepDescription = "CreateUserWizard_CustomizeCreateUserStepDescription";

        internal const string CreateUserWizard_CustomizeCompleteStep = "CreateUserWizard_CustomizeCompleteStep";

        internal const string CreateUserWizard_CustomizeCompleteStepDescription = "CreateUserWizard_CustomizeCompleteStepDescription";

        internal const string CreateUserWizard_ResetCreateUserStepVerb = "CreateUserWizard_ResetCreateUserStepVerb";

        internal const string CreateUserWizard_ResetCreateUserStepVerbDescription = "CreateUserWizard_ResetCreateUserStepVerbDescription";

        internal const string CreateUserWizard_ResetCompleteStepVerb = "CreateUserWizard_ResetCompleteStepVerb";

        internal const string CreateUserWizard_ResetCompleteStepVerbDescription = "CreateUserWizard_ResetCompleteStepVerbDescription";

        internal const string CreateUserWizard_NavigateToStep = "CreateUserWizard_NavigateToStep";

        internal const string CreateUserWizardAutoFormat_UserName = "CreateUserWizardAutoFormat_UserName";

        internal const string CreateUserWizardAutoFormat_HelpPageText = "CreateUserWizardAutoFormat_HelpPageText";

        internal const string CreateUserWizardStepCollectionEditor_Caption = "CreateUserWizardStepCollectionEditor_Caption";

        internal const string Wizard_ConvertToStartNavigationTemplate = "Wizard_ConvertToStartNavigationTemplate";

        internal const string Wizard_ConvertToStepNavigationTemplate = "Wizard_ConvertToStepNavigationTemplate";

        internal const string Wizard_ConvertToFinishNavigationTemplate = "Wizard_ConvertToFinishNavigationTemplate";

        internal const string Wizard_ConvertToSideBarTemplate = "Wizard_ConvertToSideBarTemplate";

        internal const string Wizard_ConvertToCustomNavigationTemplate = "Wizard_ConvertToCustomNavigationTemplate";

        internal const string Wizard_ConvertToTemplateDescription = "Wizard_ConvertToTemplateDescription";

        internal const string Wizard_ResetCustomNavigationTemplate = "Wizard_ResetCustomNavigationTemplate";

        internal const string Wizard_ResetStartNavigationTemplate = "Wizard_ResetStartNavigationTemplate";

        internal const string Wizard_ResetStepNavigationTemplate = "Wizard_ResetStepNavigationTemplate";

        internal const string Wizard_ResetFinishNavigationTemplate = "Wizard_ResetFinishNavigationTemplate";

        internal const string Wizard_ResetSideBarTemplate = "Wizard_ResetSideBarTemplate";

        internal const string Wizard_ResetDescription = "Wizard_ResetDescription";

        internal const string Wizard_StartWizardStepCollectionEditor = "Wizard_StartWizardStepCollectionEditor";

        internal const string Wizard_StartWizardStepCollectionEditorDescription = "Wizard_StartWizardStepCollectionEditorDescription";

        internal const string Wizard_OnViewChanged = "Wizard_OnViewChanged";

        internal const string Wizard_InvalidRegion = "Wizard_InvalidRegion";

        internal const string UIServiceHelper_ErrorCaption = "UIServiceHelper_ErrorCaption";

        internal const string Designer_DataBindingsVerb = "Designer_DataBindingsVerb";

        internal const string Designer_DataBindingsVerbDesc = "Designer_DataBindingsVerbDesc";

        internal const string MdbDataFileEditor_Ellipses = "MdbDataFileEditor_Ellipses";

        internal const string MdbDataFileEditor_Caption = "MdbDataFileEditor_Caption";

        internal const string MdbDataFileEditor_Filter = "MdbDataFileEditor_Filter";

        internal const string XmlDataFileEditor_Ellipses = "XmlDataFileEditor_Ellipses";

        internal const string XmlDataFileEditor_Caption = "XmlDataFileEditor_Caption";

        internal const string XmlDataFileEditor_Filter = "XmlDataFileEditor_Filter";

        internal const string XsdSchemaFileEditor_Ellipses = "XsdSchemaFileEditor_Ellipses";

        internal const string XsdSchemaFileEditor_Caption = "XsdSchemaFileEditor_Caption";

        internal const string XsdSchemaFileEditor_Filter = "XsdSchemaFileEditor_Filter";

        internal const string XslTransformFileEditor_Ellipses = "XslTransformFileEditor_Ellipses";

        internal const string XslTransformFileEditor_Caption = "XslTransformFileEditor_Caption";

        internal const string XslTransformFileEditor_Filter = "XslTransformFileEditor_Filter";

        internal const string UserControlFileEditor_Caption = "UserControlFileEditor_Caption";

        internal const string UserControlFileEditor_Filter = "UserControlFileEditor_Filter";

        internal const string ConnectionStringEditor_Title = "ConnectionStringEditor_Title";

        internal const string ConnectionStringEditor_HelpLabel = "ConnectionStringEditor_HelpLabel";

        internal const string ConnectionStringEditor_NewConnection = "ConnectionStringEditor_NewConnection";

        internal const string ConfigureDataSource_Title = "ConfigureDataSource_Title";

        internal const string DataSource_DebugService_FailedCall = "DataSource_DebugService_FailedCall";

        internal const string DataSource_CannotResumeEvents = "DataSource_CannotResumeEvents";

        internal const string DataSource_ConfigureTransactionDescription = "DataSource_ConfigureTransactionDescription";

        internal const string DataSourceDesigner_RefreshSchema = "DataSourceDesigner_RefreshSchema";

        internal const string DataSourceDesigner_RefreshSchemaNoHotkey = "DataSourceDesigner_RefreshSchemaNoHotkey";

        internal const string DataSourceDesigner_DataActionGroup = "DataSourceDesigner_DataActionGroup";

        internal const string DataSourceDesigner_ConfigureDataSourceVerb = "DataSourceDesigner_ConfigureDataSourceVerb";

        internal const string DataSourceDesigner_RefreshSchemaVerb = "DataSourceDesigner_RefreshSchemaVerb";

        internal const string DataSourceDesigner_ConfigureDataSourceVerbDesc = "DataSourceDesigner_ConfigureDataSourceVerbDesc";

        internal const string DataSourceDesigner_RefreshSchemaVerbDesc = "DataSourceDesigner_RefreshSchemaVerbDesc";

        internal const string HierarchicalDataBoundControlDesigner_SampleRoot = "HierarchicalDataBoundControlDesigner_SampleRoot";

        internal const string HierarchicalDataBoundControlDesigner_SampleParent = "HierarchicalDataBoundControlDesigner_SampleParent";

        internal const string HierarchicalDataBoundControlDesigner_SampleLeaf = "HierarchicalDataBoundControlDesigner_SampleLeaf";

        internal const string SqlDataSourceQueryConverter_Text = "SqlDataSourceQueryConverter_Text";

        internal const string SqlDataSourceDesigner_EditQueryTransactionDescription = "SqlDataSourceDesigner_EditQueryTransactionDescription";

        internal const string SqlDataSourceDesigner_DeleteQuery = "SqlDataSourceDesigner_DeleteQuery";

        internal const string SqlDataSourceDesigner_InsertQuery = "SqlDataSourceDesigner_InsertQuery";

        internal const string SqlDataSourceDesigner_SelectQuery = "SqlDataSourceDesigner_SelectQuery";

        internal const string SqlDataSourceDesigner_SelectCountQuery = "SqlDataSourceDesigner_SelectCountQuery";

        internal const string SqlDataSourceDesigner_UpdateQuery = "SqlDataSourceDesigner_UpdateQuery";

        internal const string SqlDataSourceDesigner_CannotGetSchema = "SqlDataSourceDesigner_CannotGetSchema";

        internal const string SqlDataSourceDesigner_CouldNotCreateConnection = "SqlDataSourceDesigner_CouldNotCreateConnection";

        internal const string SqlDataSourceDesigner_NoCommand = "SqlDataSourceDesigner_NoCommand";

        internal const string SqlDataSourceDesigner_InferStoredProcedureNotSupported = "SqlDataSourceDesigner_InferStoredProcedureNotSupported";

        internal const string SqlDataSourceDesigner_InferStoredProcedureError = "SqlDataSourceDesigner_InferStoredProcedureError";

        internal const string SqlDataSourceDesigner_RefreshSchemaRequiresSettings = "SqlDataSourceDesigner_RefreshSchemaRequiresSettings";

        internal const string SqlDataSource_General_PreviewLabel = "SqlDataSource_General_PreviewLabel";

        internal const string SqlDataSourceRefreshSchemaForm_Title = "SqlDataSourceRefreshSchemaForm_Title";

        internal const string SqlDataSourceRefreshSchemaForm_HelpLabel = "SqlDataSourceRefreshSchemaForm_HelpLabel";

        internal const string SqlDataSourceRefreshSchemaForm_ParametersLabel = "SqlDataSourceRefreshSchemaForm_ParametersLabel";

        internal const string SqlDataSourceConnectionPanel_ProviderNotFound = "SqlDataSourceConnectionPanel_ProviderNotFound";

        internal const string SqlDataSourceConnectionPanel_CouldNotGetConnectionSchema = "SqlDataSourceConnectionPanel_CouldNotGetConnectionSchema";

        internal const string SqlDataSourceSaveConfiguredConnectionPanel_HelpLabel = "SqlDataSourceSaveConfiguredConnectionPanel_HelpLabel";

        internal const string SqlDataSourceSaveConfiguredConnectionPanel_NameTextBoxDescription = "SqlDataSourceSaveConfiguredConnectionPanel_NameTextBoxDescription";

        internal const string SqlDataSourceSaveConfiguredConnectionPanel_SaveLabel = "SqlDataSourceSaveConfiguredConnectionPanel_SaveLabel";

        internal const string SqlDataSourceSaveConfiguredConnectionPanel_SaveCheckBox = "SqlDataSourceSaveConfiguredConnectionPanel_SaveCheckBox";

        internal const string SqlDataSourceSaveConfiguredConnectionPanel_PanelCaption = "SqlDataSourceSaveConfiguredConnectionPanel_PanelCaption";

        internal const string SqlDataSourceSaveConfiguredConnectionPanel_DuplicateName = "SqlDataSourceSaveConfiguredConnectionPanel_DuplicateName";

        internal const string SqlDataSourceSaveConfiguredConnectionPanel_CouldNotSaveConnection = "SqlDataSourceSaveConfiguredConnectionPanel_CouldNotSaveConnection";

        internal const string SqlDataSourceDataConnectionChooserPanel_PanelCaption = "SqlDataSourceDataConnectionChooserPanel_PanelCaption";

        internal const string SqlDataSourceDataConnectionChooserPanel_NewConnectionButton = "SqlDataSourceDataConnectionChooserPanel_NewConnectionButton";

        internal const string SqlDataSourceDataConnectionChooserPanel_ChooseLabel = "SqlDataSourceDataConnectionChooserPanel_ChooseLabel";

        internal const string SqlDataSourceDataConnectionChooserPanel_ConnectionStringLabel = "SqlDataSourceDataConnectionChooserPanel_ConnectionStringLabel";

        internal const string SqlDataSourceDataConnectionChooserPanel_CustomConnectionName = "SqlDataSourceDataConnectionChooserPanel_CustomConnectionName";

        internal const string SqlDataSourceDataConnectionChooserPanel_DetailsButtonName = "SqlDataSourceDataConnectionChooserPanel_DetailsButtonName";

        internal const string SqlDataSourceDataConnectionChooserPanel_DetailsButtonDesc = "SqlDataSourceDataConnectionChooserPanel_DetailsButtonDesc";

        internal const string SqlDataSourceQueryEditorForm_CommandLabel = "SqlDataSourceQueryEditorForm_CommandLabel";

        internal const string SqlDataSourceQueryEditorForm_InferParametersButton = "SqlDataSourceQueryEditorForm_InferParametersButton";

        internal const string SqlDataSourceQueryEditorForm_QueryBuilderButton = "SqlDataSourceQueryEditorForm_QueryBuilderButton";

        internal const string SqlDataSourceQueryEditorForm_Caption = "SqlDataSourceQueryEditorForm_Caption";

        internal const string SqlDataSourceQueryEditorForm_InferNeedsCommand = "SqlDataSourceQueryEditorForm_InferNeedsCommand";

        internal const string SqlDataSourceQueryEditorForm_QueryBuilderNeedsConnectionString = "SqlDataSourceQueryEditorForm_QueryBuilderNeedsConnectionString";

        internal const string SqlDataSourceConfigureFilterForm_ColumnLabel = "SqlDataSourceConfigureFilterForm_ColumnLabel";

        internal const string SqlDataSourceConfigureFilterForm_OperatorLabel = "SqlDataSourceConfigureFilterForm_OperatorLabel";

        internal const string SqlDataSourceConfigureFilterForm_ExpressionLabel = "SqlDataSourceConfigureFilterForm_ExpressionLabel";

        internal const string SqlDataSourceConfigureFilterForm_ValueLabel = "SqlDataSourceConfigureFilterForm_ValueLabel";

        internal const string SqlDataSourceConfigureFilterForm_ExpressionColumnHeader = "SqlDataSourceConfigureFilterForm_ExpressionColumnHeader";

        internal const string SqlDataSourceConfigureFilterForm_ValueColumnHeader = "SqlDataSourceConfigureFilterForm_ValueColumnHeader";

        internal const string SqlDataSourceConfigureFilterForm_ParameterPropertiesGroup = "SqlDataSourceConfigureFilterForm_ParameterPropertiesGroup";

        internal const string SqlDataSourceConfigureFilterForm_SourceLabel = "SqlDataSourceConfigureFilterForm_SourceLabel";

        internal const string SqlDataSourceConfigureFilterForm_WhereLabel = "SqlDataSourceConfigureFilterForm_WhereLabel";

        internal const string SqlDataSourceConfigureFilterForm_AddButton = "SqlDataSourceConfigureFilterForm_AddButton";

        internal const string SqlDataSourceConfigureFilterForm_HelpLabel = "SqlDataSourceConfigureFilterForm_HelpLabel";

        internal const string SqlDataSourceConfigureFilterForm_RemoveButton = "SqlDataSourceConfigureFilterForm_RemoveButton";

        internal const string SqlDataSourceConfigureFilterForm_Caption = "SqlDataSourceConfigureFilterForm_Caption";

        internal const string SqlDataSourceConfigureFilterForm_ParameterEditor_DefaultValue = "SqlDataSourceConfigureFilterForm_ParameterEditor_DefaultValue";

        internal const string SqlDataSourceConfigureFilterForm_StaticParameterEditor_ValueLabel = "SqlDataSourceConfigureFilterForm_StaticParameterEditor_ValueLabel";

        internal const string SqlDataSourceConfigureFilterForm_CookieParameterEditor_CookieNameLabel = "SqlDataSourceConfigureFilterForm_CookieParameterEditor_CookieNameLabel";

        internal const string SqlDataSourceConfigureFilterForm_ControlParameterEditor_ControlIDLabel = "SqlDataSourceConfigureFilterForm_ControlParameterEditor_ControlIDLabel";

        internal const string SqlDataSourceConfigureFilterForm_FormParameterEditor_FormFieldLabel = "SqlDataSourceConfigureFilterForm_FormParameterEditor_FormFieldLabel";

        internal const string SqlDataSourceConfigureFilterForm_QueryStringParameterEditor_QueryStringFieldLabel = "SqlDataSourceConfigureFilterForm_QueryStringParameterEditor_QueryStringFieldLabel";

        internal const string SqlDataSourceConfigureFilterForm_RouteParameterEditor_RouteKeyLabel = "SqlDataSourceConfigureFilterForm_RouteParameterEditor_RouteKeyLabel";

        internal const string SqlDataSourceConfigureFilterForm_SessionParameterEditor_SessionFieldLabel = "SqlDataSourceConfigureFilterForm_SessionParameterEditor_SessionFieldLabel";

        internal const string SqlDataSourceConfigureFilterForm_ProfileParameterEditor_PropertyNameLabel = "SqlDataSourceConfigureFilterForm_ProfileParameterEditor_PropertyNameLabel";

        internal const string SqlDataSourceConfigureSortForm_HelpLabel = "SqlDataSourceConfigureSortForm_HelpLabel";

        internal const string SqlDataSourceConfigureSortForm_SortByLabel = "SqlDataSourceConfigureSortForm_SortByLabel";

        internal const string SqlDataSourceConfigureSortForm_ThenByLabel = "SqlDataSourceConfigureSortForm_ThenByLabel";

        internal const string SqlDataSourceConfigureSortForm_AscendingLabel = "SqlDataSourceConfigureSortForm_AscendingLabel";

        internal const string SqlDataSourceConfigureSortForm_DescendingLabel = "SqlDataSourceConfigureSortForm_DescendingLabel";

        internal const string SqlDataSourceConfigureSortForm_Caption = "SqlDataSourceConfigureSortForm_Caption";

        internal const string SqlDataSourceConfigureSortForm_SortDirection1 = "SqlDataSourceConfigureSortForm_SortDirection1";

        internal const string SqlDataSourceConfigureSortForm_SortDirection2 = "SqlDataSourceConfigureSortForm_SortDirection2";

        internal const string SqlDataSourceConfigureSortForm_SortDirection3 = "SqlDataSourceConfigureSortForm_SortDirection3";

        internal const string SqlDataSourceConfigureSortForm_SortColumn1 = "SqlDataSourceConfigureSortForm_SortColumn1";

        internal const string SqlDataSourceConfigureSortForm_SortColumn2 = "SqlDataSourceConfigureSortForm_SortColumn2";

        internal const string SqlDataSourceConfigureSortForm_SortColumn3 = "SqlDataSourceConfigureSortForm_SortColumn3";

        internal const string SqlDataSourceConfigureSortForm_SortNone = "SqlDataSourceConfigureSortForm_SortNone";

        internal const string SqlDataSourceConfigureParametersPanel_PanelCaption = "SqlDataSourceConfigureParametersPanel_PanelCaption";

        internal const string SqlDataSourceConfigureParametersPanel_HelpLabel = "SqlDataSourceConfigureParametersPanel_HelpLabel";

        internal const string SqlDataSourceSummaryPanel_PanelCaption = "SqlDataSourceSummaryPanel_PanelCaption";

        internal const string SqlDataSourceSummaryPanel_TestQueryButton = "SqlDataSourceSummaryPanel_TestQueryButton";

        internal const string SqlDataSourceSummaryPanel_HelpLabel = "SqlDataSourceSummaryPanel_HelpLabel";

        internal const string SqlDataSourceSummaryPanel_ResultsAccessibleName = "SqlDataSourceSummaryPanel_ResultsAccessibleName";

        internal const string SqlDataSourceSummaryPanel_CouldNotCreateConnection = "SqlDataSourceSummaryPanel_CouldNotCreateConnection";

        internal const string SqlDataSourceSummaryPanel_CannotExecuteQueryNoTables = "SqlDataSourceSummaryPanel_CannotExecuteQueryNoTables";

        internal const string SqlDataSourceSummaryPanel_CannotExecuteQuery = "SqlDataSourceSummaryPanel_CannotExecuteQuery";

        internal const string SqlDataSourceConfigureSelectPanel_PanelCaption = "SqlDataSourceConfigureSelectPanel_PanelCaption";

        internal const string SqlDataSourceConfigureSelectPanel_RetrieveDataLabel = "SqlDataSourceConfigureSelectPanel_RetrieveDataLabel";

        internal const string SqlDataSourceConfigureSelectPanel_TableLabel = "SqlDataSourceConfigureSelectPanel_TableLabel";

        internal const string SqlDataSourceConfigureSelectPanel_CustomSqlLabel = "SqlDataSourceConfigureSelectPanel_CustomSqlLabel";

        internal const string SqlDataSourceConfigureSelectPanel_TableNameLabel = "SqlDataSourceConfigureSelectPanel_TableNameLabel";

        internal const string SqlDataSourceConfigureSelectPanel_FieldsLabel = "SqlDataSourceConfigureSelectPanel_FieldsLabel";

        internal const string SqlDataSourceConfigureSelectPanel_SortButton = "SqlDataSourceConfigureSelectPanel_SortButton";

        internal const string SqlDataSourceConfigureSelectPanel_FilterLabel = "SqlDataSourceConfigureSelectPanel_FilterLabel";

        internal const string SqlDataSourceConfigureSelectPanel_SelectDistinctLabel = "SqlDataSourceConfigureSelectPanel_SelectDistinctLabel";

        internal const string SqlDataSourceConfigureSelectPanel_AdvancedOptions = "SqlDataSourceConfigureSelectPanel_AdvancedOptions";

        internal const string SqlDataSourceConfigureSelectPanel_CouldNotGetTableSchema = "SqlDataSourceConfigureSelectPanel_CouldNotGetTableSchema";

        internal const string SqlDataSourceAdvancedOptionsForm_HelpLabel = "SqlDataSourceAdvancedOptionsForm_HelpLabel";

        internal const string SqlDataSourceAdvancedOptionsForm_GenerateCheckBox = "SqlDataSourceAdvancedOptionsForm_GenerateCheckBox";

        internal const string SqlDataSourceAdvancedOptionsForm_GenerateHelpLabel = "SqlDataSourceAdvancedOptionsForm_GenerateHelpLabel";

        internal const string SqlDataSourceAdvancedOptionsForm_OptimisticCheckBox = "SqlDataSourceAdvancedOptionsForm_OptimisticCheckBox";

        internal const string SqlDataSourceAdvancedOptionsForm_OptimisticLabel = "SqlDataSourceAdvancedOptionsForm_OptimisticLabel";

        internal const string SqlDataSourceAdvancedOptionsForm_Caption = "SqlDataSourceAdvancedOptionsForm_Caption";

        internal const string SqlDataSourceCustomCommandEditor_QueryBuilderButton = "SqlDataSourceCustomCommandEditor_QueryBuilderButton";

        internal const string SqlDataSourceCustomCommandEditor_SqlLabel = "SqlDataSourceCustomCommandEditor_SqlLabel";

        internal const string SqlDataSourceCustomCommandEditor_StoredProcedureLabel = "SqlDataSourceCustomCommandEditor_StoredProcedureLabel";

        internal const string SqlDataSourceCustomCommandEditor_NoConnectionString = "SqlDataSourceCustomCommandEditor_NoConnectionString";

        internal const string SqlDataSourceCustomCommandEditor_CouldNotGetStoredProcedureSchema = "SqlDataSourceCustomCommandEditor_CouldNotGetStoredProcedureSchema";

        internal const string SqlDataSourceCustomCommandPanel_HelpLabel = "SqlDataSourceCustomCommandPanel_HelpLabel";

        internal const string SqlDataSourceCustomCommandPanel_PanelCaption = "SqlDataSourceCustomCommandPanel_PanelCaption";

        internal const string SqlDataSourceParameterValueEditorForm_HelpLabel = "SqlDataSourceParameterValueEditorForm_HelpLabel";

        internal const string SqlDataSourceParameterValueEditorForm_ParametersGridAccessibleName = "SqlDataSourceParameterValueEditorForm_ParametersGridAccessibleName";

        internal const string SqlDataSourceParameterValueEditorForm_Caption = "SqlDataSourceParameterValueEditorForm_Caption";

        internal const string SqlDataSourceParameterValueEditorForm_DbTypeColumnHeader = "SqlDataSourceParameterValueEditorForm_DbTypeColumnHeader";

        internal const string SqlDataSourceParameterValueEditorForm_ParameterColumnHeader = "SqlDataSourceParameterValueEditorForm_ParameterColumnHeader";

        internal const string SqlDataSourceParameterValueEditorForm_TypeColumnHeader = "SqlDataSourceParameterValueEditorForm_TypeColumnHeader";

        internal const string SqlDataSourceParameterValueEditorForm_ValueColumnHeader = "SqlDataSourceParameterValueEditorForm_ValueColumnHeader";

        internal const string SqlDataSourceParameterValueEditorForm_InvalidParameter = "SqlDataSourceParameterValueEditorForm_InvalidParameter";

        internal const string AccessDataSourceConnectionChooserPanel_PanelCaption = "AccessDataSourceConnectionChooserPanel_PanelCaption";

        internal const string AccessDataSourceConnectionChooserPanel_DataFileLabel = "AccessDataSourceConnectionChooserPanel_DataFileLabel";

        internal const string AccessDataSourceConnectionChooserPanel_HelpLabel = "AccessDataSourceConnectionChooserPanel_HelpLabel";

        internal const string AccessDataSourceConnectionChooserPanel_BrowseButton = "AccessDataSourceConnectionChooserPanel_BrowseButton";

        internal const string AccessDataSourceConnectionChooserPanel_FileNotFound = "AccessDataSourceConnectionChooserPanel_FileNotFound";

        internal const string XmlDataSourceConfigureDataSourceForm_HelpLabel = "XmlDataSourceConfigureDataSourceForm_HelpLabel";

        internal const string XmlDataSourceConfigureDataSourceForm_DataFileLabel = "XmlDataSourceConfigureDataSourceForm_DataFileLabel";

        internal const string XmlDataSourceConfigureDataSourceForm_TransformFileLabel = "XmlDataSourceConfigureDataSourceForm_TransformFileLabel";

        internal const string XmlDataSourceConfigureDataSourceForm_TransformFileHelpLabel = "XmlDataSourceConfigureDataSourceForm_TransformFileHelpLabel";

        internal const string XmlDataSourceConfigureDataSourceForm_XPathExpressionLabel = "XmlDataSourceConfigureDataSourceForm_XPathExpressionLabel";

        internal const string XmlDataSourceConfigureDataSourceForm_XPathExpressionHelpLabel = "XmlDataSourceConfigureDataSourceForm_XPathExpressionHelpLabel";

        internal const string XmlDataSourceConfigureDataSourceForm_Browse = "XmlDataSourceConfigureDataSourceForm_Browse";

        internal const string ObjectDataSourceDesigner_CannotGetSchema = "ObjectDataSourceDesigner_CannotGetSchema";

        internal const string ObjectDataSourceDesigner_CannotGetType = "ObjectDataSourceDesigner_CannotGetType";

        internal const string ObjectDataSource_General_MethodSignatureLabel = "ObjectDataSource_General_MethodSignatureLabel";

        internal const string ObjectDataSourceConfigureParametersPanel_PanelCaption = "ObjectDataSourceConfigureParametersPanel_PanelCaption";

        internal const string ObjectDataSourceConfigureParametersPanel_HelpLabel = "ObjectDataSourceConfigureParametersPanel_HelpLabel";

        internal const string ObjectDataSourceChooseMethodsPanel_PanelCaption = "ObjectDataSourceChooseMethodsPanel_PanelCaption";

        internal const string ObjectDataSourceChooseMethodsPanel_IncompatibleDataObjectTypes = "ObjectDataSourceChooseMethodsPanel_IncompatibleDataObjectTypes";

        internal const string ObjectDataSourceMethodEditor_DeleteHelpLabel = "ObjectDataSourceMethodEditor_DeleteHelpLabel";

        internal const string ObjectDataSourceMethodEditor_InsertHelpLabel = "ObjectDataSourceMethodEditor_InsertHelpLabel";

        internal const string ObjectDataSourceMethodEditor_SelectHelpLabel = "ObjectDataSourceMethodEditor_SelectHelpLabel";

        internal const string ObjectDataSourceMethodEditor_UpdateHelpLabel = "ObjectDataSourceMethodEditor_UpdateHelpLabel";

        internal const string ObjectDataSourceMethodEditor_MethodLabel = "ObjectDataSourceMethodEditor_MethodLabel";

        internal const string ObjectDataSourceMethodEditor_SignatureFormat = "ObjectDataSourceMethodEditor_SignatureFormat";

        internal const string ObjectDataSourceMethodEditor_NoMethod = "ObjectDataSourceMethodEditor_NoMethod";

        internal const string ObjectDataSourceChooseTypePanel_PanelCaption = "ObjectDataSourceChooseTypePanel_PanelCaption";

        internal const string ObjectDataSourceChooseTypePanel_HelpLabel = "ObjectDataSourceChooseTypePanel_HelpLabel";

        internal const string ObjectDataSourceChooseTypePanel_NameLabel = "ObjectDataSourceChooseTypePanel_NameLabel";

        internal const string ObjectDataSourceChooseTypePanel_ExampleLabel = "ObjectDataSourceChooseTypePanel_ExampleLabel";

        internal const string ObjectDataSourceChooseTypePanel_FilterCheckBox = "ObjectDataSourceChooseTypePanel_FilterCheckBox";

        internal const string ParameterCollectionEditor_InvalidParameters = "ParameterCollectionEditor_InvalidParameters";

        internal const string ParameterCollectionEditorForm_Caption = "ParameterCollectionEditorForm_Caption";

        internal const string ParameterEditorUserControl_ParametersLabel = "ParameterEditorUserControl_ParametersLabel";

        internal const string ParameterEditorUserControl_PropertiesLabel = "ParameterEditorUserControl_PropertiesLabel";

        internal const string ParameterEditorUserControl_AddButton = "ParameterEditorUserControl_AddButton";

        internal const string ParameterEditorUserControl_SourceLabel = "ParameterEditorUserControl_SourceLabel";

        internal const string ParameterEditorUserControl_ParameterNameColumnHeader = "ParameterEditorUserControl_ParameterNameColumnHeader";

        internal const string ParameterEditorUserControl_ParameterValueColumnHeader = "ParameterEditorUserControl_ParameterValueColumnHeader";

        internal const string ParameterEditorUserControl_MoveParameterUp = "ParameterEditorUserControl_MoveParameterUp";

        internal const string ParameterEditorUserControl_MoveParameterDown = "ParameterEditorUserControl_MoveParameterDown";

        internal const string ParameterEditorUserControl_DeleteParameter = "ParameterEditorUserControl_DeleteParameter";

        internal const string ParameterEditorUserControl_ControlParameterExpressionUnknown = "ParameterEditorUserControl_ControlParameterExpressionUnknown";

        internal const string ParameterEditorUserControl_CookieParameterExpressionUnknown = "ParameterEditorUserControl_CookieParameterExpressionUnknown";

        internal const string ParameterEditorUserControl_FormParameterExpressionUnknown = "ParameterEditorUserControl_FormParameterExpressionUnknown";

        internal const string ParameterEditorUserControl_QueryStringParameterExpressionUnknown = "ParameterEditorUserControl_QueryStringParameterExpressionUnknown";

        internal const string ParameterEditorUserControl_SessionParameterExpressionUnknown = "ParameterEditorUserControl_SessionParameterExpressionUnknown";

        internal const string ParameterEditorUserControl_ProfileParameterExpressionUnknown = "ParameterEditorUserControl_ProfileParameterExpressionUnknown";

        internal const string ParameterEditorUserControl_RouteParameterExpressionUnknown = "ParameterEditorUserControl_RouteParameterExpressionUnknown";

        internal const string ParameterEditorUserControl_ShowAdvancedProperties = "ParameterEditorUserControl_ShowAdvancedProperties";

        internal const string ParameterEditorUserControl_HideAdvancedPropertiesLabel = "ParameterEditorUserControl_HideAdvancedPropertiesLabel";

        internal const string ParameterEditorUserControl_AdvancedProperties = "ParameterEditorUserControl_AdvancedProperties";

        internal const string ParameterEditorUserControl_ParameterDefaultValue = "ParameterEditorUserControl_ParameterDefaultValue";

        internal const string ParameterEditorUserControl_ControlParameterControlID = "ParameterEditorUserControl_ControlParameterControlID";

        internal const string ParameterEditorUserControl_CookieParameterCookieName = "ParameterEditorUserControl_CookieParameterCookieName";

        internal const string ParameterEditorUserControl_FormParameterFormField = "ParameterEditorUserControl_FormParameterFormField";

        internal const string ParameterEditorUserControl_SessionParameterSessionField = "ParameterEditorUserControl_SessionParameterSessionField";

        internal const string ParameterEditorUserControl_QueryStringParameterQueryStringField = "ParameterEditorUserControl_QueryStringParameterQueryStringField";

        internal const string ParameterEditorUserControl_ProfilePropertyName = "ParameterEditorUserControl_ProfilePropertyName";

        internal const string ParameterEditorUserControl_RouteParameterRouteKey = "ParameterEditorUserControl_RouteParameterRouteKey";

        internal const string DBDlg_Text = "DBDlg_Text";

        internal const string DBDlg_Inst = "DBDlg_Inst";

        internal const string DBDlg_BindableProps = "DBDlg_BindableProps";

        internal const string DBDlg_ShowAll = "DBDlg_ShowAll";

        internal const string DBDlg_TwoWay = "DBDlg_TwoWay";

        internal const string DBDlg_OK = "DBDlg_OK";

        internal const string DBDlg_Cancel = "DBDlg_Cancel";

        internal const string DBDlg_Help = "DBDlg_Help";

        internal const string DBDlg_BindingGroup = "DBDlg_BindingGroup";

        internal const string DBDlg_FieldBinding = "DBDlg_FieldBinding";

        internal const string DBDlg_Field = "DBDlg_Field";

        internal const string DBDlg_Format = "DBDlg_Format";

        internal const string DBDlg_Sample = "DBDlg_Sample";

        internal const string DBDlg_CustomBinding = "DBDlg_CustomBinding";

        internal const string DBDlg_Expr = "DBDlg_Expr";

        internal const string DBDlg_RefreshSchema = "DBDlg_RefreshSchema";

        internal const string DBDlg_Unbound = "DBDlg_Unbound";

        internal const string DBDlg_Fmt_None = "DBDlg_Fmt_None";

        internal const string DBDlg_Fmt_General = "DBDlg_Fmt_General";

        internal const string DBDlg_Fmt_ShortDate = "DBDlg_Fmt_ShortDate";

        internal const string DBDlg_Fmt_LongDate = "DBDlg_Fmt_LongDate";

        internal const string DBDlg_Fmt_ShortTime = "DBDlg_Fmt_ShortTime";

        internal const string DBDlg_Fmt_LongTime = "DBDlg_Fmt_LongTime";

        internal const string DBDlg_Fmt_DateTime = "DBDlg_Fmt_DateTime";

        internal const string DBDlg_Fmt_FullDate = "DBDlg_Fmt_FullDate";

        internal const string DBDlg_Fmt_Decimal = "DBDlg_Fmt_Decimal";

        internal const string DBDlg_Fmt_Numeric = "DBDlg_Fmt_Numeric";

        internal const string DBDlg_Fmt_Fixed = "DBDlg_Fmt_Fixed";

        internal const string DBDlg_Fmt_Currency = "DBDlg_Fmt_Currency";

        internal const string DBDlg_Fmt_Scientific = "DBDlg_Fmt_Scientific";

        internal const string DBDlg_Fmt_Hexadecimal = "DBDlg_Fmt_Hexadecimal";

        internal const string DBDlg_InvalidFormat = "DBDlg_InvalidFormat";

        internal const string ExpressionBindingsDialog_Text = "ExpressionBindingsDialog_Text";

        internal const string ExpressionBindingsDialog_None = "ExpressionBindingsDialog_None";

        internal const string ExpressionBindingsDialog_Inst = "ExpressionBindingsDialog_Inst";

        internal const string ExpressionBindingsDialog_BindableProps = "ExpressionBindingsDialog_BindableProps";

        internal const string ExpressionBindingsDialog_OK = "ExpressionBindingsDialog_OK";

        internal const string ExpressionBindingsDialog_Cancel = "ExpressionBindingsDialog_Cancel";

        internal const string ExpressionBindingsDialog_ExpressionType = "ExpressionBindingsDialog_ExpressionType";

        internal const string ExpressionBindingsDialog_Properties = "ExpressionBindingsDialog_Properties";

        internal const string ExpressionBindingsDialog_UndefinedExpressionPrefix = "ExpressionBindingsDialog_UndefinedExpressionPrefix";

        internal const string ExpressionBindingsDialog_GeneratedExpression = "ExpressionBindingsDialog_GeneratedExpression";

        internal const string BDL_PrivateDataSource = "BDL_PrivateDataSource";

        internal const string BDL_PrivateDataSourceT = "BDL_PrivateDataSourceT";

        internal const string BDL_TemplateModePropBuilder = "BDL_TemplateModePropBuilder";

        internal const string BDL_PropertyBuilder = "BDL_PropertyBuilder";

        internal const string BDL_PropertyBuilderVerb = "BDL_PropertyBuilderVerb";

        internal const string BDL_PropertyBuilderDesc = "BDL_PropertyBuilderDesc";

        internal const string BDL_BehaviorGroup = "BDL_BehaviorGroup";

        internal const string BDLAF_Title = "BDLAF_Title";

        internal const string BDLAF_SchemeName = "BDLAF_SchemeName";

        internal const string BDLAF_Preview = "BDLAF_Preview";

        internal const string BDLAF_OK = "BDLAF_OK";

        internal const string BDLAF_Cancel = "BDLAF_Cancel";

        internal const string BDLAF_Help = "BDLAF_Help";

        internal const string BDLAF_Column1 = "BDLAF_Column1";

        internal const string BDLAF_Column2 = "BDLAF_Column2";

        internal const string BDLAF_Header = "BDLAF_Header";

        internal const string BDLAF_Footer = "BDLAF_Footer";

        internal const string BDLAF_Apply = "BDLAF_Apply";

        internal const string BDLAF_AutoFormats = "BDLAF_AutoFormats";

        internal const string BDLAF_Skins = "BDLAF_Skins";

        internal const string BDLAF_DefaultSkin = "BDLAF_DefaultSkin";

        internal const string BDLAF_NoSkin = "BDLAF_NoSkin";

        internal const string BDLAF_Couldnotgeneratepreview = "BDLAF_Couldnotgeneratepreview";

        internal const string BDLAF_RemoveFormatting = "BDLAF_RemoveFormatting";

        internal const string BDLScheme_Empty = "BDLScheme_Empty";

        internal const string BDLScheme_Colorful1 = "BDLScheme_Colorful1";

        internal const string BDLScheme_Colorful2 = "BDLScheme_Colorful2";

        internal const string BDLScheme_Colorful3 = "BDLScheme_Colorful3";

        internal const string BDLScheme_Colorful4 = "BDLScheme_Colorful4";

        internal const string BDLScheme_Colorful5 = "BDLScheme_Colorful5";

        internal const string BDLScheme_Professional1 = "BDLScheme_Professional1";

        internal const string BDLScheme_Professional2 = "BDLScheme_Professional2";

        internal const string BDLScheme_Professional3 = "BDLScheme_Professional3";

        internal const string BDLScheme_Simple1 = "BDLScheme_Simple1";

        internal const string BDLScheme_Simple2 = "BDLScheme_Simple2";

        internal const string BDLScheme_Simple3 = "BDLScheme_Simple3";

        internal const string BDLScheme_Classic1 = "BDLScheme_Classic1";

        internal const string BDLScheme_Classic2 = "BDLScheme_Classic2";

        internal const string BDLScheme_Consistent1 = "BDLScheme_Consistent1";

        internal const string BDLScheme_Consistent2 = "BDLScheme_Consistent2";

        internal const string BDLScheme_Consistent3 = "BDLScheme_Consistent3";

        internal const string BDLScheme_Consistent4 = "BDLScheme_Consistent4";

        internal const string BDLBor_Text = "BDLBor_Text";

        internal const string BDLBor_Desc = "BDLBor_Desc";

        internal const string BDLBor_CellMarginsGroup = "BDLBor_CellMarginsGroup";

        internal const string BDLBor_CellPadding = "BDLBor_CellPadding";

        internal const string BDLBor_CellSpacing = "BDLBor_CellSpacing";

        internal const string BDLBor_BorderLinesGroup = "BDLBor_BorderLinesGroup";

        internal const string BDLBor_GridLines = "BDLBor_GridLines";

        internal const string BDLBor_GL_Horz = "BDLBor_GL_Horz";

        internal const string BDLBor_GL_Vert = "BDLBor_GL_Vert";

        internal const string BDLBor_GL_Both = "BDLBor_GL_Both";

        internal const string BDLBor_GL_None = "BDLBor_GL_None";

        internal const string BDLBor_BorderColor = "BDLBor_BorderColor";

        internal const string BDLBor_BorderWidth = "BDLBor_BorderWidth";

        internal const string BDLBor_ChooseColorButton = "BDLBor_ChooseColorButton";

        internal const string BDLBor_ChooseColorDesc = "BDLBor_ChooseColorDesc";

        internal const string BDLBor_BorderWidthValueDesc = "BDLBor_BorderWidthValueDesc";

        internal const string BDLBor_BorderWidthValueName = "BDLBor_BorderWidthValueName";

        internal const string BDLBor_BorderWidthUnitDesc = "BDLBor_BorderWidthUnitDesc";

        internal const string BDLBor_BorderWidthUnitName = "BDLBor_BorderWidthUnitName";

        internal const string BDLFmt_Text = "BDLFmt_Text";

        internal const string BDLFmt_Desc = "BDLFmt_Desc";

        internal const string BDLFmt_Objects = "BDLFmt_Objects";

        internal const string BDLFmt_AppearanceGroup = "BDLFmt_AppearanceGroup";

        internal const string BDLFmt_ForeColor = "BDLFmt_ForeColor";

        internal const string BDLFmt_BackColor = "BDLFmt_BackColor";

        internal const string BDLFmt_FontName = "BDLFmt_FontName";

        internal const string BDLFmt_FontSize = "BDLFmt_FontSize";

        internal const string BDLFmt_FS_Smaller = "BDLFmt_FS_Smaller";

        internal const string BDLFmt_FS_Larger = "BDLFmt_FS_Larger";

        internal const string BDLFmt_FS_XXSmall = "BDLFmt_FS_XXSmall";

        internal const string BDLFmt_FS_XSmall = "BDLFmt_FS_XSmall";

        internal const string BDLFmt_FS_Small = "BDLFmt_FS_Small";

        internal const string BDLFmt_FS_Medium = "BDLFmt_FS_Medium";

        internal const string BDLFmt_FS_Large = "BDLFmt_FS_Large";

        internal const string BDLFmt_FS_XLarge = "BDLFmt_FS_XLarge";

        internal const string BDLFmt_FS_XXLarge = "BDLFmt_FS_XXLarge";

        internal const string BDLFmt_FS_Custom = "BDLFmt_FS_Custom";

        internal const string BDLFmt_FontBold = "BDLFmt_FontBold";

        internal const string BDLFmt_FontItalic = "BDLFmt_FontItalic";

        internal const string BDLFmt_FontUnderline = "BDLFmt_FontUnderline";

        internal const string BDLFmt_FontStrikeout = "BDLFmt_FontStrikeout";

        internal const string BDLFmt_FontOverline = "BDLFmt_FontOverline";

        internal const string BDLFmt_AlignmentGroup = "BDLFmt_AlignmentGroup";

        internal const string BDLFmt_HorzAlign = "BDLFmt_HorzAlign";

        internal const string BDLFmt_HA_Left = "BDLFmt_HA_Left";

        internal const string BDLFmt_HA_Center = "BDLFmt_HA_Center";

        internal const string BDLFmt_HA_Right = "BDLFmt_HA_Right";

        internal const string BDLFmt_HA_Justify = "BDLFmt_HA_Justify";

        internal const string BDLFmt_VertAlign = "BDLFmt_VertAlign";

        internal const string BDLFmt_VA_Top = "BDLFmt_VA_Top";

        internal const string BDLFmt_VA_Middle = "BDLFmt_VA_Middle";

        internal const string BDLFmt_VA_Bottom = "BDLFmt_VA_Bottom";

        internal const string BDLFmt_LayoutGroup = "BDLFmt_LayoutGroup";

        internal const string BDLFmt_Width = "BDLFmt_Width";

        internal const string BDLFmt_AllowWrapping = "BDLFmt_AllowWrapping";

        internal const string BDLFmt_Node_EntireDG = "BDLFmt_Node_EntireDG";

        internal const string BDLFmt_Node_EntireDL = "BDLFmt_Node_EntireDL";

        internal const string BDLFmt_Node_Header = "BDLFmt_Node_Header";

        internal const string BDLFmt_Node_Footer = "BDLFmt_Node_Footer";

        internal const string BDLFmt_Node_Pager = "BDLFmt_Node_Pager";

        internal const string BDLFmt_Node_Items = "BDLFmt_Node_Items";

        internal const string BDLFmt_Node_Separators = "BDLFmt_Node_Separators";

        internal const string BDLFmt_Node_NormalItems = "BDLFmt_Node_NormalItems";

        internal const string BDLFmt_Node_AltItems = "BDLFmt_Node_AltItems";

        internal const string BDLFmt_Node_SelItems = "BDLFmt_Node_SelItems";

        internal const string BDLFmt_Node_EditItems = "BDLFmt_Node_EditItems";

        internal const string BDLFmt_Node_Columns = "BDLFmt_Node_Columns";

        internal const string BDLFmt_ChooseColorButton = "BDLFmt_ChooseColorButton";

        internal const string BDLFmt_ChooseForeColorDesc = "BDLFmt_ChooseForeColorDesc";

        internal const string BDLFmt_ChooseBackColorDesc = "BDLFmt_ChooseBackColorDesc";

        internal const string BDLFmt_FontSizeValueDesc = "BDLFmt_FontSizeValueDesc";

        internal const string BDLFmt_FontSizeValueName = "BDLFmt_FontSizeValueName";

        internal const string BDLFmt_FontSizeUnitDesc = "BDLFmt_FontSizeUnitDesc";

        internal const string BDLFmt_FontSizeUnitName = "BDLFmt_FontSizeUnitName";

        internal const string BDLFmt_WidthValueDesc = "BDLFmt_WidthValueDesc";

        internal const string BDLFmt_WidthValueName = "BDLFmt_WidthValueName";

        internal const string BDLFmt_WidthUnitDesc = "BDLFmt_WidthUnitDesc";

        internal const string BDLFmt_WidthUnitName = "BDLFmt_WidthUnitName";

        internal const string CalAFmt_Title = "CalAFmt_Title";

        internal const string CalAFmt_SchemeName = "CalAFmt_SchemeName";

        internal const string CalAFmt_Preview = "CalAFmt_Preview";

        internal const string CalAFmt_OK = "CalAFmt_OK";

        internal const string CalAFmt_Cancel = "CalAFmt_Cancel";

        internal const string CalAFmt_Help = "CalAFmt_Help";

        internal const string CalAFmt_Scheme_Default = "CalAFmt_Scheme_Default";

        internal const string CalAFmt_Scheme_Simple = "CalAFmt_Scheme_Simple";

        internal const string CalAFmt_Scheme_Professional1 = "CalAFmt_Scheme_Professional1";

        internal const string CalAFmt_Scheme_Professional2 = "CalAFmt_Scheme_Professional2";

        internal const string CalAFmt_Scheme_Classic = "CalAFmt_Scheme_Classic";

        internal const string CalAFmt_Scheme_Colorful1 = "CalAFmt_Scheme_Colorful1";

        internal const string CalAFmt_Scheme_Colorful2 = "CalAFmt_Scheme_Colorful2";

        internal const string CreateDataSource_Title = "CreateDataSource_Title";

        internal const string CreateDataSource_Caption = "CreateDataSource_Caption";

        internal const string CreateDataSource_Description = "CreateDataSource_Description";

        internal const string CreateDataSource_SelectType = "CreateDataSource_SelectType";

        internal const string CreateDataSource_SelectTypeDesc = "CreateDataSource_SelectTypeDesc";

        internal const string CreateDataSource_ID = "CreateDataSource_ID";

        internal const string CreateDataSource_NameNotValid = "CreateDataSource_NameNotValid";

        internal const string CreateDataSource_NameNotUnique = "CreateDataSource_NameNotUnique";

        internal const string DataSourceIDChromeConverter_NoDataSource = "DataSourceIDChromeConverter_NoDataSource";

        internal const string DataSourceIDChromeConverter_NewDataSource = "DataSourceIDChromeConverter_NewDataSource";

        internal const string DCFAdd_Title = "DCFAdd_Title";

        internal const string DCFAdd_ChooseField = "DCFAdd_ChooseField";

        internal const string DCFAdd_HeaderText = "DCFAdd_HeaderText";

        internal const string DCFAdd_DataField = "DCFAdd_DataField";

        internal const string DCFAdd_ButtonType = "DCFAdd_ButtonType";

        internal const string DCFAdd_CommandName = "DCFAdd_CommandName";

        internal const string DCFAdd_Text = "DCFAdd_Text";

        internal const string DCFAdd_CommandButtons = "DCFAdd_CommandButtons";

        internal const string DCFAdd_EditUpdate = "DCFAdd_EditUpdate";

        internal const string DCFAdd_Delete = "DCFAdd_Delete";

        internal const string DCFAdd_NewInsert = "DCFAdd_NewInsert";

        internal const string DCFAdd_Select = "DCFAdd_Select";

        internal const string DCFAdd_ShowCancel = "DCFAdd_ShowCancel";

        internal const string DCFAdd_DeleteDesc = "DCFAdd_DeleteDesc";

        internal const string DCFAdd_SelectDesc = "DCFAdd_SelectDesc";

        internal const string DCFAdd_ShowCancelDesc = "DCFAdd_ShowCancelDesc";

        internal const string DCFAdd_EditUpdateDesc = "DCFAdd_EditUpdateDesc";

        internal const string DCFAdd_NewInsertDesc = "DCFAdd_NewInsertDesc";

        internal const string DCFAdd_ReadOnly = "DCFAdd_ReadOnly";

        internal const string DCFAdd_ImageMode = "DCFAdd_ImageMode";

        internal const string DCFAdd_DataMode = "DCFAdd_DataMode";

        internal const string DCFAdd_LinkMode = "DCFAdd_LinkMode";

        internal const string DCFAdd_LinkFormatString = "DCFAdd_LinkFormatString";

        internal const string DCFAdd_ExampleFormatString = "DCFAdd_ExampleFormatString";

        internal const string DCFAdd_HyperlinkText = "DCFAdd_HyperlinkText";

        internal const string DCFAdd_HyperlinkURL = "DCFAdd_HyperlinkURL";

        internal const string DCFAdd_SpecifyText = "DCFAdd_SpecifyText";

        internal const string DCFAdd_BindText = "DCFAdd_BindText";

        internal const string DCFAdd_TextFormatString = "DCFAdd_TextFormatString";

        internal const string DCFAdd_TextFormatStringExample = "DCFAdd_TextFormatStringExample";

        internal const string DCFAdd_SpecifyURL = "DCFAdd_SpecifyURL";

        internal const string DCFAdd_BindURL = "DCFAdd_BindURL";

        internal const string DCFAdd_URLFormatString = "DCFAdd_URLFormatString";

        internal const string DCFAdd_URLFormatStringExample = "DCFAdd_URLFormatStringExample";

        internal const string DCFEditor_Text = "DCFEditor_Text";

        internal const string DCFEditor_AutoGen = "DCFEditor_AutoGen";

        internal const string DCFEditor_AvailableFields = "DCFEditor_AvailableFields";

        internal const string DCFEditor_SelectedFields = "DCFEditor_SelectedFields";

        internal const string DCFEditor_FieldProps = "DCFEditor_FieldProps";

        internal const string DCFEditor_FieldPropsFormat = "DCFEditor_FieldPropsFormat";

        internal const string DCFEditor_Add = "DCFEditor_Add";

        internal const string DCFEditor_MoveFieldUpName = "DCFEditor_MoveFieldUpName";

        internal const string DCFEditor_MoveFieldDownName = "DCFEditor_MoveFieldDownName";

        internal const string DCFEditor_DeleteFieldName = "DCFEditor_DeleteFieldName";

        internal const string DCFEditor_MoveFieldUpDesc = "DCFEditor_MoveFieldUpDesc";

        internal const string DCFEditor_MoveFieldDownDesc = "DCFEditor_MoveFieldDownDesc";

        internal const string DCFEditor_DeleteFieldDesc = "DCFEditor_DeleteFieldDesc";

        internal const string DCFEditor_Templatize = "DCFEditor_Templatize";

        internal const string DCFEditor_Node_AllFields = "DCFEditor_Node_AllFields";

        internal const string DCFEditor_Node_Bound = "DCFEditor_Node_Bound";

        internal const string DCFEditor_Node_Button = "DCFEditor_Node_Button";

        internal const string DCFEditor_Node_Command = "DCFEditor_Node_Command";

        internal const string DCFEditor_Node_CheckBox = "DCFEditor_Node_CheckBox";

        internal const string DCFEditor_Node_HyperLink = "DCFEditor_Node_HyperLink";

        internal const string DCFEditor_Node_Template = "DCFEditor_Node_Template";

        internal const string DCFEditor_Node_Select = "DCFEditor_Node_Select";

        internal const string DCFEditor_Node_Edit = "DCFEditor_Node_Edit";

        internal const string DCFEditor_Node_Insert = "DCFEditor_Node_Insert";

        internal const string DCFEditor_Node_Delete = "DCFEditor_Node_Delete";

        internal const string DCFEditor_Node_Image = "DCFEditor_Node_Image";

        internal const string DCFEditor_Button = "DCFEditor_Button";

        internal const string DCFEditor_HyperLink = "DCFEditor_HyperLink";

        internal const string DesignTimeSiteMapProvider_RootNodeText = "DesignTimeSiteMapProvider_RootNodeText";

        internal const string DesignTimeSiteMapProvider_ParentNodeText = "DesignTimeSiteMapProvider_ParentNodeText";

        internal const string DesignTimeSiteMapProvider_SiblingNodeText = "DesignTimeSiteMapProvider_SiblingNodeText";

        internal const string DesignTimeSiteMapProvider_CurrentNodeText = "DesignTimeSiteMapProvider_CurrentNodeText";

        internal const string DesignTimeSiteMapProvider_ChildNodeText = "DesignTimeSiteMapProvider_ChildNodeText";

        internal const string DesignTimeSiteMapProvider_Duplicate_Url = "DesignTimeSiteMapProvider_Duplicate_Url";

        internal const string DGGen_Text = "DGGen_Text";

        internal const string DGGen_Desc = "DGGen_Desc";

        internal const string DGGen_DataGroup = "DGGen_DataGroup";

        internal const string DGGen_DataSource = "DGGen_DataSource";

        internal const string DGGen_DataMember = "DGGen_DataMember";

        internal const string DGGen_DSUnbound = "DGGen_DSUnbound";

        internal const string DGGen_DataKey = "DGGen_DataKey";

        internal const string DGGen_DKNone = "DGGen_DKNone";

        internal const string DGGen_DMNone = "DGGen_DMNone";

        internal const string DGGen_HeaderFooterGroup = "DGGen_HeaderFooterGroup";

        internal const string DGGen_ShowHeader = "DGGen_ShowHeader";

        internal const string DGGen_ShowFooter = "DGGen_ShowFooter";

        internal const string DGGen_BehaviorGroup = "DGGen_BehaviorGroup";

        internal const string DGGen_AllowSorting = "DGGen_AllowSorting";

        internal const string DGGen_AutoColumnInfo = "DGGen_AutoColumnInfo";

        internal const string DGGen_CustomColumnInfo = "DGGen_CustomColumnInfo";

        internal const string DGPg_Text = "DGPg_Text";

        internal const string DGPg_Desc = "DGPg_Desc";

        internal const string DGPg_PagingGroup = "DGPg_PagingGroup";

        internal const string DGPg_AllowPaging = "DGPg_AllowPaging";

        internal const string DGPg_AllowCustomPaging = "DGPg_AllowCustomPaging";

        internal const string DGPg_PageSize = "DGPg_PageSize";

        internal const string DGPg_Rows = "DGPg_Rows";

        internal const string DGPg_NavigationGroup = "DGPg_NavigationGroup";

        internal const string DGPg_Visible = "DGPg_Visible";

        internal const string DGPg_Position = "DGPg_Position";

        internal const string DGPg_Pos_Top = "DGPg_Pos_Top";

        internal const string DGPg_Pos_Bottom = "DGPg_Pos_Bottom";

        internal const string DGPg_Pos_TopBottom = "DGPg_Pos_TopBottom";

        internal const string DGPg_Mode = "DGPg_Mode";

        internal const string DGPg_Mode_Buttons = "DGPg_Mode_Buttons";

        internal const string DGPg_Mode_Numbers = "DGPg_Mode_Numbers";

        internal const string DGPg_NextPage = "DGPg_NextPage";

        internal const string DGPg_PrevPage = "DGPg_PrevPage";

        internal const string DGPg_ButtonCount = "DGPg_ButtonCount";

        internal const string DGCol_Text = "DGCol_Text";

        internal const string DGCol_Desc = "DGCol_Desc";

        internal const string DGCol_AutoGen = "DGCol_AutoGen";

        internal const string DGCol_ColListGroup = "DGCol_ColListGroup";

        internal const string DGCol_AvailableCols = "DGCol_AvailableCols";

        internal const string DGCol_SelectedCols = "DGCol_SelectedCols";

        internal const string DGCol_ColumnPropsGroup1 = "DGCol_ColumnPropsGroup1";

        internal const string DGCol_ColumnPropsGroup2 = "DGCol_ColumnPropsGroup2";

        internal const string DGCol_HeaderText = "DGCol_HeaderText";

        internal const string DGCol_HeaderImage = "DGCol_HeaderImage";

        internal const string DGCol_FooterText = "DGCol_FooterText";

        internal const string DGCol_SortExpr = "DGCol_SortExpr";

        internal const string DGCol_Visible = "DGCol_Visible";

        internal const string DGCol_Templatize = "DGCol_Templatize";

        internal const string DGCol_Node = "DGCol_Node";

        internal const string DGCol_Node_DataFields = "DGCol_Node_DataFields";

        internal const string DGCol_Node_AllFields = "DGCol_Node_AllFields";

        internal const string DGCol_Node_Bound = "DGCol_Node_Bound";

        internal const string DGCol_Node_Button = "DGCol_Node_Button";

        internal const string DGCol_Node_Select = "DGCol_Node_Select";

        internal const string DGCol_Node_Edit = "DGCol_Node_Edit";

        internal const string DGCol_Node_Delete = "DGCol_Node_Delete";

        internal const string DGCol_Node_HyperLink = "DGCol_Node_HyperLink";

        internal const string DGCol_Node_Template = "DGCol_Node_Template";

        internal const string DGCol_DFC_DataField = "DGCol_DFC_DataField";

        internal const string DGCol_DFC_DataFormat = "DGCol_DFC_DataFormat";

        internal const string DGCol_DFC_ReadOnly = "DGCol_DFC_ReadOnly";

        internal const string DGCol_BC_Text = "DGCol_BC_Text";

        internal const string DGCol_BC_DataTextField = "DGCol_BC_DataTextField";

        internal const string DGCol_BC_DataTextFormat = "DGCol_BC_DataTextFormat";

        internal const string DGCol_BC_Command = "DGCol_BC_Command";

        internal const string DGCol_BC_ButtonType = "DGCol_BC_ButtonType";

        internal const string DGCol_BC_BT_Link = "DGCol_BC_BT_Link";

        internal const string DGCol_BC_BT_Push = "DGCol_BC_BT_Push";

        internal const string DGCol_HC_Text = "DGCol_HC_Text";

        internal const string DGCol_HC_DataTextField = "DGCol_HC_DataTextField";

        internal const string DGCol_HC_DataTextFormat = "DGCol_HC_DataTextFormat";

        internal const string DGCol_HC_URL = "DGCol_HC_URL";

        internal const string DGCol_HC_DataURLField = "DGCol_HC_DataURLField";

        internal const string DGCol_HC_DataURLFormat = "DGCol_HC_DataURLFormat";

        internal const string DGCol_HC_Target = "DGCol_HC_Target";

        internal const string DGCol_EC_Edit = "DGCol_EC_Edit";

        internal const string DGCol_EC_Update = "DGCol_EC_Update";

        internal const string DGCol_EC_Cancel = "DGCol_EC_Cancel";

        internal const string DGCol_EC_ButtonType = "DGCol_EC_ButtonType";

        internal const string DGCol_EC_BT_Link = "DGCol_EC_BT_Link";

        internal const string DGCol_EC_BT_Push = "DGCol_EC_BT_Push";

        internal const string DGCol_Button = "DGCol_Button";

        internal const string DGCol_SelectButton = "DGCol_SelectButton";

        internal const string DGCol_DeleteButton = "DGCol_DeleteButton";

        internal const string DGCol_EditButton = "DGCol_EditButton";

        internal const string DGCol_UpdateButton = "DGCol_UpdateButton";

        internal const string DGCol_CancelButton = "DGCol_CancelButton";

        internal const string DGCol_HyperLink = "DGCol_HyperLink";

        internal const string DGCol_URLPFilter = "DGCol_URLPFilter";

        internal const string DGCol_URLPCaption = "DGCol_URLPCaption";

        internal const string DGCol_AddColButtonDesc = "DGCol_AddColButtonDesc";

        internal const string DGCol_MoveColumnUpButtonDesc = "DGCol_MoveColumnUpButtonDesc";

        internal const string DGCol_MoveColumnDownButtonDesc = "DGCol_MoveColumnDownButtonDesc";

        internal const string DGCol_DeleteColumnButtonDesc = "DGCol_DeleteColumnButtonDesc";

        internal const string DGCol_HeaderImagePickerDesc = "DGCol_HeaderImagePickerDesc";

        internal const string DataList_NoTemplatesInst = "DataList_NoTemplatesInst";

        internal const string DataList_NoTemplatesInst2 = "DataList_NoTemplatesInst2";

        internal const string DataList_HeaderFooterTemplates = "DataList_HeaderFooterTemplates";

        internal const string DataList_ItemTemplates = "DataList_ItemTemplates";

        internal const string DataList_SeparatorTemplate = "DataList_SeparatorTemplate";

        internal const string DataList_RefreshSchemaTransaction = "DataList_RefreshSchemaTransaction";

        internal const string DataList_RegenerateTemplates = "DataList_RegenerateTemplates";

        internal const string DataList_ClearTemplates = "DataList_ClearTemplates";

        internal const string DataList_ClearTemplatesCaption = "DataList_ClearTemplatesCaption";

        internal const string DLGen_Text = "DLGen_Text";

        internal const string DLGen_Desc = "DLGen_Desc";

        internal const string DLGen_DataGroup = "DLGen_DataGroup";

        internal const string DLGen_DataSource = "DLGen_DataSource";

        internal const string DLGen_DataMember = "DLGen_DataMember";

        internal const string DLGen_DSUnbound = "DLGen_DSUnbound";

        internal const string DLGen_DataKey = "DLGen_DataKey";

        internal const string DLGen_DKNone = "DLGen_DKNone";

        internal const string DLGen_DMNone = "DLGen_DMNone";

        internal const string DLGen_HeaderFooterGroup = "DLGen_HeaderFooterGroup";

        internal const string DLGen_ShowHeader = "DLGen_ShowHeader";

        internal const string DLGen_ShowFooter = "DLGen_ShowFooter";

        internal const string DLGen_RepeatLayoutGroup = "DLGen_RepeatLayoutGroup";

        internal const string DLGen_RepeatColumns = "DLGen_RepeatColumns";

        internal const string DLGen_RepeatDirection = "DLGen_RepeatDirection";

        internal const string DLGen_RD_Horz = "DLGen_RD_Horz";

        internal const string DLGen_RD_Vert = "DLGen_RD_Vert";

        internal const string DLGen_RepeatLayout = "DLGen_RepeatLayout";

        internal const string DLGen_RL_Table = "DLGen_RL_Table";

        internal const string DLGen_RL_Flow = "DLGen_RL_Flow";

        internal const string DLGen_ExtractRows = "DLGen_ExtractRows";

        internal const string DLGen_Templates = "DLGen_Templates";

        internal const string DVScheme_Empty = "DVScheme_Empty";

        internal const string DVScheme_Colorful1 = "DVScheme_Colorful1";

        internal const string DVScheme_Colorful2 = "DVScheme_Colorful2";

        internal const string DVScheme_Colorful3 = "DVScheme_Colorful3";

        internal const string DVScheme_Colorful4 = "DVScheme_Colorful4";

        internal const string DVScheme_Colorful5 = "DVScheme_Colorful5";

        internal const string DVScheme_Professional1 = "DVScheme_Professional1";

        internal const string DVScheme_Professional2 = "DVScheme_Professional2";

        internal const string DVScheme_Professional3 = "DVScheme_Professional3";

        internal const string DVScheme_Simple1 = "DVScheme_Simple1";

        internal const string DVScheme_Simple2 = "DVScheme_Simple2";

        internal const string DVScheme_Simple3 = "DVScheme_Simple3";

        internal const string DVScheme_Classic1 = "DVScheme_Classic1";

        internal const string DVScheme_Classic2 = "DVScheme_Classic2";

        internal const string DVScheme_Consistent1 = "DVScheme_Consistent1";

        internal const string DVScheme_Consistent2 = "DVScheme_Consistent2";

        internal const string DVScheme_Consistent3 = "DVScheme_Consistent3";

        internal const string DVScheme_Consistent4 = "DVScheme_Consistent4";

        internal const string FVScheme_Empty = "FVScheme_Empty";

        internal const string FVScheme_Colorful1 = "FVScheme_Colorful1";

        internal const string FVScheme_Colorful2 = "FVScheme_Colorful2";

        internal const string FVScheme_Colorful3 = "FVScheme_Colorful3";

        internal const string FVScheme_Colorful4 = "FVScheme_Colorful4";

        internal const string FVScheme_Colorful5 = "FVScheme_Colorful5";

        internal const string FVScheme_Professional1 = "FVScheme_Professional1";

        internal const string FVScheme_Professional2 = "FVScheme_Professional2";

        internal const string FVScheme_Professional3 = "FVScheme_Professional3";

        internal const string FVScheme_Simple1 = "FVScheme_Simple1";

        internal const string FVScheme_Simple2 = "FVScheme_Simple2";

        internal const string FVScheme_Simple3 = "FVScheme_Simple3";

        internal const string FVScheme_Classic1 = "FVScheme_Classic1";

        internal const string FVScheme_Classic2 = "FVScheme_Classic2";

        internal const string FVScheme_Consistent1 = "FVScheme_Consistent1";

        internal const string FVScheme_Consistent2 = "FVScheme_Consistent2";

        internal const string FVScheme_Consistent3 = "FVScheme_Consistent3";

        internal const string FVScheme_Consistent4 = "FVScheme_Consistent4";

        internal const string Repeater_NoTemplatesInst = "Repeater_NoTemplatesInst";

        internal const string BaseDataBoundControl_CreateDataSourceTransaction = "BaseDataBoundControl_CreateDataSourceTransaction";

        internal const string BaseDataBoundControl_ConfigureDataVerb = "BaseDataBoundControl_ConfigureDataVerb";

        internal const string BaseDataBoundControl_ConfigureDataVerbDesc = "BaseDataBoundControl_ConfigureDataVerbDesc";

        internal const string BaseDataBoundControl_DataActionGroup = "BaseDataBoundControl_DataActionGroup";

        internal const string ExpressionEditor_ExpressionBound = "ExpressionEditor_ExpressionBound";

        internal const string AppSettingExpressionEditor_AppSetting = "AppSettingExpressionEditor_AppSetting";

        internal const string ConnectionStringsExpressionEditor_ConnectionName = "ConnectionStringsExpressionEditor_ConnectionName";

        internal const string ConnectionStringsExpressionEditor_ConnectionType = "ConnectionStringsExpressionEditor_ConnectionType";

        internal const string ExpressionEditor_Expression = "ExpressionEditor_Expression";

        internal const string ResourceExpressionEditorSheet_ClassKey = "ResourceExpressionEditorSheet_ClassKey";

        internal const string ResourceExpressionEditorSheet_ResourceKey = "ResourceExpressionEditorSheet_ResourceKey";

        internal const string ResourceExpressionEditorSheet_InvalidResourceKey = "ResourceExpressionEditorSheet_InvalidResourceKey";

        internal const string RouteValueExpressionEditorSheet_RouteValue = "RouteValueExpressionEditorSheet_RouteValue";

        internal const string RouteUrlExpressionEditor_InvalidExpression = "RouteUrlExpressionEditor_InvalidExpression";

        internal const string RouteUrlExpressionEditorSheet_RouteName = "RouteUrlExpressionEditorSheet_RouteName";

        internal const string RouteUrlExpressionEditorSheet_RouteValues = "RouteUrlExpressionEditorSheet_RouteValues";

        internal const string ControlDesigner_WndProcException = "ControlDesigner_WndProcException";

        internal const string DataBoundControl_SchemaRefreshedWarning = "DataBoundControl_SchemaRefreshedWarning";

        internal const string DataBoundControl_SchemaRefreshedWarningNoDataSource = "DataBoundControl_SchemaRefreshedWarningNoDataSource";

        internal const string DataBoundControl_SchemaRefreshedCaption = "DataBoundControl_SchemaRefreshedCaption";

        internal const string DataBoundControl_GridView = "DataBoundControl_GridView";

        internal const string DataBoundControl_DetailsView = "DataBoundControl_DetailsView";

        internal const string DataBoundControl_FormView = "DataBoundControl_FormView";

        internal const string DataBoundControl_Column = "DataBoundControl_Column";

        internal const string DataBoundControl_Row = "DataBoundControl_Row";

        internal const string DataBoundControlActionList_SetDataSourceIDTransaction = "DataBoundControlActionList_SetDataSourceIDTransaction";

        internal const string GridView_EditFieldsTransaction = "GridView_EditFieldsTransaction";

        internal const string GridView_AddNewFieldTransaction = "GridView_AddNewFieldTransaction";

        internal const string GridView_EnableEditingTransaction = "GridView_EnableEditingTransaction";

        internal const string GridView_EnableDeletingTransaction = "GridView_EnableDeletingTransaction";

        internal const string GridView_EnableSortingTransaction = "GridView_EnableSortingTransaction";

        internal const string GridView_EnableSelectionTransaction = "GridView_EnableSelectionTransaction";

        internal const string GridView_EnablePagingTransaction = "GridView_EnablePagingTransaction";

        internal const string GridView_MoveLeftTransaction = "GridView_MoveLeftTransaction";

        internal const string GridView_MoveRightTransaction = "GridView_MoveRightTransaction";

        internal const string GridView_RemoveFieldTransaction = "GridView_RemoveFieldTransaction";

        internal const string GridView_SchemaRefreshedTransaction = "GridView_SchemaRefreshedTransaction";

        internal const string GridView_EditFieldsVerb = "GridView_EditFieldsVerb";

        internal const string GridView_AddNewFieldVerb = "GridView_AddNewFieldVerb";

        internal const string GridView_RemoveFieldVerb = "GridView_RemoveFieldVerb";

        internal const string GridView_MoveFieldLeftVerb = "GridView_MoveFieldLeftVerb";

        internal const string GridView_MoveFieldRightVerb = "GridView_MoveFieldRightVerb";

        internal const string GridView_EditFieldsDesc = "GridView_EditFieldsDesc";

        internal const string GridView_AddNewFieldDesc = "GridView_AddNewFieldDesc";

        internal const string GridView_RemoveFieldDesc = "GridView_RemoveFieldDesc";

        internal const string GridView_MoveFieldLeftDesc = "GridView_MoveFieldLeftDesc";

        internal const string GridView_MoveFieldRightDesc = "GridView_MoveFieldRightDesc";

        internal const string GridView_Field = "GridView_Field";

        internal const string GridView_EnablePaging = "GridView_EnablePaging";

        internal const string GridView_EnableSorting = "GridView_EnableSorting";

        internal const string GridView_EnableEditing = "GridView_EnableEditing";

        internal const string GridView_EnableDeleting = "GridView_EnableDeleting";

        internal const string GridView_EnableSelection = "GridView_EnableSelection";

        internal const string GridView_EnablePagingDesc = "GridView_EnablePagingDesc";

        internal const string GridView_EnableSortingDesc = "GridView_EnableSortingDesc";

        internal const string GridView_EnableEditingDesc = "GridView_EnableEditingDesc";

        internal const string GridView_EnableDeletingDesc = "GridView_EnableDeletingDesc";

        internal const string GridView_EnableSelectionDesc = "GridView_EnableSelectionDesc";

        internal const string DataControls_SchemaRefreshedTransaction = "DataControls_SchemaRefreshedTransaction";

        internal const string DetailsView_EditFieldsTransaction = "DetailsView_EditFieldsTransaction";

        internal const string DetailsView_AddNewFieldTransaction = "DetailsView_AddNewFieldTransaction";

        internal const string DetailsView_EnableEditingTransaction = "DetailsView_EnableEditingTransaction";

        internal const string DetailsView_EnableDeletingTransaction = "DetailsView_EnableDeletingTransaction";

        internal const string DetailsView_EnableInsertingTransaction = "DetailsView_EnableInsertingTransaction";

        internal const string DetailsView_EnablePagingTransaction = "DetailsView_EnablePagingTransaction";

        internal const string DetailsView_MoveUpTransaction = "DetailsView_MoveUpTransaction";

        internal const string DetailsView_MoveDownTransaction = "DetailsView_MoveDownTransaction";

        internal const string DetailsView_RemoveFieldTransaction = "DetailsView_RemoveFieldTransaction";

        internal const string DetailsView_EditFieldsVerb = "DetailsView_EditFieldsVerb";

        internal const string DetailsView_AddNewFieldVerb = "DetailsView_AddNewFieldVerb";

        internal const string DetailsView_RemoveFieldVerb = "DetailsView_RemoveFieldVerb";

        internal const string DetailsView_MoveFieldUpVerb = "DetailsView_MoveFieldUpVerb";

        internal const string DetailsView_MoveFieldDownVerb = "DetailsView_MoveFieldDownVerb";

        internal const string DetailsView_Field = "DetailsView_Field";

        internal const string DetailsView_EnablePaging = "DetailsView_EnablePaging";

        internal const string DetailsView_EnableEditing = "DetailsView_EnableEditing";

        internal const string DetailsView_EnableDeleting = "DetailsView_EnableDeleting";

        internal const string DetailsView_EnableInserting = "DetailsView_EnableInserting";

        internal const string DetailsView_EditFieldsDesc = "DetailsView_EditFieldsDesc";

        internal const string DetailsView_AddNewFieldDesc = "DetailsView_AddNewFieldDesc";

        internal const string DetailsView_RemoveFieldDesc = "DetailsView_RemoveFieldDesc";

        internal const string DetailsView_MoveFieldUpDesc = "DetailsView_MoveFieldUpDesc";

        internal const string DetailsView_MoveFieldDownDesc = "DetailsView_MoveFieldDownDesc";

        internal const string DetailsView_EnablePagingDesc = "DetailsView_EnablePagingDesc";

        internal const string DetailsView_EnableEditingDesc = "DetailsView_EnableEditingDesc";

        internal const string DetailsView_EnableDeletingDesc = "DetailsView_EnableDeletingDesc";

        internal const string DetailsView_EnableInsertingDesc = "DetailsView_EnableInsertingDesc";

        internal const string FormView_EnablePagingTransaction = "FormView_EnablePagingTransaction";

        internal const string FormView_EnablePaging = "FormView_EnablePaging";

        internal const string FormView_EnablePagingDesc = "FormView_EnablePagingDesc";

        internal const string FormView_EnableDynamicData = "FormView_EnableDynamicData";

        internal const string FormView_EnableDynamicDataDesc = "FormView_EnableDynamicDataDesc";

        internal const string FormView_SchemaRefreshedWarning = "FormView_SchemaRefreshedWarning";

        internal const string FormView_SchemaRefreshedWarningNoDataSource = "FormView_SchemaRefreshedWarningNoDataSource";

        internal const string FormView_SchemaRefreshedWarningGenerate = "FormView_SchemaRefreshedWarningGenerate";

        internal const string FormView_SchemaRefreshedCaption = "FormView_SchemaRefreshedCaption";

        internal const string FormView_Edit = "FormView_Edit";

        internal const string FormView_Update = "FormView_Update";

        internal const string FormView_Cancel = "FormView_Cancel";

        internal const string FormView_Delete = "FormView_Delete";

        internal const string FormView_New = "FormView_New";

        internal const string FormView_Insert = "FormView_Insert";

        internal const string ListControlCreateDataSource_Title = "ListControlCreateDataSource_Title";

        internal const string ListControlCreateDataSource_Caption = "ListControlCreateDataSource_Caption";

        internal const string ListControlCreateDataSource_Description = "ListControlCreateDataSource_Description";

        internal const string ListControlCreateDataSource_SelectDataSource = "ListControlCreateDataSource_SelectDataSource";

        internal const string ListControlCreateDataSource_SelectDataTextField = "ListControlCreateDataSource_SelectDataTextField";

        internal const string ListControlCreateDataSource_SelectDataValueField = "ListControlCreateDataSource_SelectDataValueField";

        internal const string ListControl_ConfigureDataVerb = "ListControl_ConfigureDataVerb";

        internal const string ListControlDesigner_ConnectToDataSource = "ListControlDesigner_ConnectToDataSource";

        internal const string ListControl_EnableAutoPostBack = "ListControl_EnableAutoPostBack";

        internal const string ListControl_EnableAutoPostBackDesc = "ListControl_EnableAutoPostBackDesc";

        internal const string ListControl_EditItems = "ListControl_EditItems";

        internal const string ListControl_EditItemsDesc = "ListControl_EditItemsDesc";

        internal const string ListControlDesigner_EditItems = "ListControlDesigner_EditItems";

        internal const string ContainerControlDesigner_RegionWatermark = "ContainerControlDesigner_RegionWatermark";

        internal const string ContentPlaceHolder_Invalid_RootComponent = "ContentPlaceHolder_Invalid_RootComponent";

        internal const string Content_CreateBlankContent = "Content_CreateBlankContent";

        internal const string Content_ClearRegion = "Content_ClearRegion";

        internal const string RenderOuterTable_RemoveOuterTableWarning = "RenderOuterTable_RemoveOuterTableWarning";

        internal const string RenderOuterTable_RemoveOuterTableCaption = "RenderOuterTable_RemoveOuterTableCaption";

        internal const string RenderOuterTableHelper_ResetProperties = "RenderOuterTableHelper_ResetProperties";

        internal const string SiteMapPathAFmt_Scheme_Default = "SiteMapPathAFmt_Scheme_Default";

        internal const string SiteMapPathAFmt_Scheme_Colorful = "SiteMapPathAFmt_Scheme_Colorful";

        internal const string SiteMapPathAFmt_Scheme_Simple = "SiteMapPathAFmt_Scheme_Simple";

        internal const string SiteMapPathAFmt_Scheme_Professional = "SiteMapPathAFmt_Scheme_Professional";

        internal const string SiteMapPathAFmt_Scheme_Classic = "SiteMapPathAFmt_Scheme_Classic";

        internal const string ImageGeneratorUrlEditor_Filter = "ImageGeneratorUrlEditor_Filter";

        internal const string WebControls_ConvertToTemplate = "WebControls_ConvertToTemplate";

        internal const string WebControls_ConvertToTemplateDescription = "WebControls_ConvertToTemplateDescription";

        internal const string WebControls_ConvertToTemplateDescriptionViews = "WebControls_ConvertToTemplateDescriptionViews";

        internal const string WebControls_Reset = "WebControls_Reset";

        internal const string WebControls_ResetDescription = "WebControls_ResetDescription";

        internal const string WebControls_ResetDescriptionViews = "WebControls_ResetDescriptionViews";

        internal const string WebControls_Views = "WebControls_Views";

        internal const string WebControls_ViewsDescription = "WebControls_ViewsDescription";

        internal const string ChangePassword_ChangePasswordView = "ChangePassword_ChangePasswordView";

        internal const string ChangePassword_SuccessView = "ChangePassword_SuccessView";

        internal const string ChangePasswordAutoFormat_UserName = "ChangePasswordAutoFormat_UserName";

        internal const string ChangePasswordAutoFormat_HelpPageText = "ChangePasswordAutoFormat_HelpPageText";

        internal const string ChangePasswordScheme_Empty = "ChangePasswordScheme_Empty";

        internal const string ChangePasswordScheme_Classic = "ChangePasswordScheme_Classic";

        internal const string ChangePasswordScheme_Elegant = "ChangePasswordScheme_Elegant";

        internal const string ChangePasswordScheme_Simple = "ChangePasswordScheme_Simple";

        internal const string ChangePasswordScheme_Professional = "ChangePasswordScheme_Professional";

        internal const string ChangePasswordScheme_Colorful = "ChangePasswordScheme_Colorful";

        internal const string Login_LaunchWebAdmin = "Login_LaunchWebAdmin";

        internal const string Login_LaunchWebAdminDescription = "Login_LaunchWebAdminDescription";

        internal const string LoginScheme_Empty = "LoginScheme_Empty";

        internal const string LoginScheme_Classic = "LoginScheme_Classic";

        internal const string LoginScheme_Elegant = "LoginScheme_Elegant";

        internal const string LoginScheme_Simple = "LoginScheme_Simple";

        internal const string LoginScheme_Professional = "LoginScheme_Professional";

        internal const string LoginScheme_Colorful = "LoginScheme_Colorful";

        internal const string LoginAutoFormat_UserName = "LoginAutoFormat_UserName";

        internal const string LoginAutoFormat_HelpPageText = "LoginAutoFormat_HelpPageText";

        internal const string CreateUserWizardScheme_Empty = "CreateUserWizardScheme_Empty";

        internal const string CreateUserWizardScheme_Classic = "CreateUserWizardScheme_Classic";

        internal const string CreateUserWizardScheme_Elegant = "CreateUserWizardScheme_Elegant";

        internal const string CreateUserWizardScheme_Simple = "CreateUserWizardScheme_Simple";

        internal const string CreateUserWizardScheme_Professional = "CreateUserWizardScheme_Professional";

        internal const string CreateUserWizardScheme_Colorful = "CreateUserWizardScheme_Colorful";

        internal const string LoginStatus_LoggedOutView = "LoginStatus_LoggedOutView";

        internal const string LoginStatus_LoggedInView = "LoginStatus_LoggedInView";

        internal const string LoginView_EditRoleGroups = "LoginView_EditRoleGroups";

        internal const string LoginView_EditRoleGroupsDescription = "LoginView_EditRoleGroupsDescription";

        internal const string LoginView_EditRoleGroupsTransactionDescription = "LoginView_EditRoleGroupsTransactionDescription";

        internal const string LoginView_ErrorRendering = "LoginView_ErrorRendering";

        internal const string LoginView_AnonymousTemplateEmpty = "LoginView_AnonymousTemplateEmpty";

        internal const string LoginView_LoggedInTemplateEmpty = "LoginView_LoggedInTemplateEmpty";

        internal const string LoginView_RoleGroupTemplateEmpty = "LoginView_RoleGroupTemplateEmpty";

        internal const string LoginView_NoTemplateInst = "LoginView_NoTemplateInst";

        internal const string UserControlDesignerHost_ComponentAlreadyExists = "UserControlDesignerHost_ComponentAlreadyExists";

        internal const string MenuDesigner_DataActionGroup = "MenuDesigner_DataActionGroup";

        internal const string MenuDesigner_EditBindingsTransactionDescription = "MenuDesigner_EditBindingsTransactionDescription";

        internal const string MenuDesigner_EditMenuItemsTransactionDescription = "MenuDesigner_EditMenuItemsTransactionDescription";

        internal const string MenuDesigner_EditBindings = "MenuDesigner_EditBindings";

        internal const string MenuDesigner_EditBindingsDescription = "MenuDesigner_EditBindingsDescription";

        internal const string MenuDesigner_EditMenuItems = "MenuDesigner_EditMenuItems";

        internal const string MenuDesigner_EditMenuItemsDescription = "MenuDesigner_EditMenuItemsDescription";

        internal const string MenuDesigner_CreateLineImages = "MenuDesigner_CreateLineImages";

        internal const string MenuDesigner_Empty = "MenuDesigner_Empty";

        internal const string MenuDesigner_EmptyDataBinding = "MenuDesigner_EmptyDataBinding";

        internal const string MenuDesigner_Error = "MenuDesigner_Error";

        internal const string MenuDesigner_EditNodesTransactionDescription = "MenuDesigner_EditNodesTransactionDescription";

        internal const string MenuDesigner_EditNodes = "MenuDesigner_EditNodes";

        internal const string MenuDesigner_ViewsDescription = "MenuDesigner_ViewsDescription";

        internal const string MenuDesigner_ConvertToDynamicTemplate = "MenuDesigner_ConvertToDynamicTemplate";

        internal const string MenuDesigner_ConvertToDynamicTemplateDescription = "MenuDesigner_ConvertToDynamicTemplateDescription";

        internal const string MenuDesigner_ResetDynamicTemplate = "MenuDesigner_ResetDynamicTemplate";

        internal const string MenuDesigner_ResetDynamicTemplateDescription = "MenuDesigner_ResetDynamicTemplateDescription";

        internal const string MenuDesigner_ConvertToStaticTemplate = "MenuDesigner_ConvertToStaticTemplate";

        internal const string MenuDesigner_ConvertToStaticTemplateDescription = "MenuDesigner_ConvertToStaticTemplateDescription";

        internal const string MenuDesigner_ResetStaticTemplate = "MenuDesigner_ResetStaticTemplate";

        internal const string MenuDesigner_ResetStaticTemplateDescription = "MenuDesigner_ResetStaticTemplateDescription";

        internal const string Menu_StaticView = "Menu_StaticView";

        internal const string Menu_DynamicView = "Menu_DynamicView";

        internal const string MenuItemCollectionEditor_AddRoot = "MenuItemCollectionEditor_AddRoot";

        internal const string MenuItemCollectionEditor_AddChild = "MenuItemCollectionEditor_AddChild";

        internal const string MenuItemCollectionEditor_Remove = "MenuItemCollectionEditor_Remove";

        internal const string MenuItemCollectionEditor_MoveDown = "MenuItemCollectionEditor_MoveDown";

        internal const string MenuItemCollectionEditor_MoveUp = "MenuItemCollectionEditor_MoveUp";

        internal const string MenuItemCollectionEditor_Indent = "MenuItemCollectionEditor_Indent";

        internal const string MenuItemCollectionEditor_Unindent = "MenuItemCollectionEditor_Unindent";

        internal const string MenuItemCollectionEditor_OK = "MenuItemCollectionEditor_OK";

        internal const string MenuItemCollectionEditor_Cancel = "MenuItemCollectionEditor_Cancel";

        internal const string MenuItemCollectionEditor_Nodes = "MenuItemCollectionEditor_Nodes";

        internal const string MenuItemCollectionEditor_Properties = "MenuItemCollectionEditor_Properties";

        internal const string MenuItemCollectionEditor_PropertyGrid = "MenuItemCollectionEditor_PropertyGrid";

        internal const string MenuItemCollectionEditor_Title = "MenuItemCollectionEditor_Title";

        internal const string MenuItemCollectionEditor_NewNodeText = "MenuItemCollectionEditor_NewNodeText";

        internal const string MenuItemCollectionEditor_CantSelect = "MenuItemCollectionEditor_CantSelect";

        internal const string MenuBindingsEditor_Apply = "MenuBindingsEditor_Apply";

        internal const string MenuBindingsEditor_AddBinding = "MenuBindingsEditor_AddBinding";

        internal const string MenuBindingsEditor_AutoGenerateBindings = "MenuBindingsEditor_AutoGenerateBindings";

        internal const string MenuBindingsEditor_Bindings = "MenuBindingsEditor_Bindings";

        internal const string MenuBindingsEditor_BindingProperties = "MenuBindingsEditor_BindingProperties";

        internal const string MenuBindingsEditor_Cancel = "MenuBindingsEditor_Cancel";

        internal const string MenuBindingsEditor_EmptyBindingText = "MenuBindingsEditor_EmptyBindingText";

        internal const string MenuBindingsEditor_OK = "MenuBindingsEditor_OK";

        internal const string MenuBindingsEditor_Schema = "MenuBindingsEditor_Schema";

        internal const string MenuBindingsEditor_Title = "MenuBindingsEditor_Title";

        internal const string MenuBindingsEditor_MoveBindingUpName = "MenuBindingsEditor_MoveBindingUpName";

        internal const string MenuBindingsEditor_MoveBindingUpDescription = "MenuBindingsEditor_MoveBindingUpDescription";

        internal const string MenuBindingsEditor_MoveBindingDownName = "MenuBindingsEditor_MoveBindingDownName";

        internal const string MenuBindingsEditor_MoveBindingDownDescription = "MenuBindingsEditor_MoveBindingDownDescription";

        internal const string MenuBindingsEditor_DeleteBindingName = "MenuBindingsEditor_DeleteBindingName";

        internal const string MenuBindingsEditor_DeleteBindingDescription = "MenuBindingsEditor_DeleteBindingDescription";

        internal const string MenuScheme_Empty = "MenuScheme_Empty";

        internal const string MenuScheme_Classic = "MenuScheme_Classic";

        internal const string MenuScheme_Professional = "MenuScheme_Professional";

        internal const string MenuScheme_Colorful = "MenuScheme_Colorful";

        internal const string MenuScheme_Simple = "MenuScheme_Simple";

        internal const string PagerScheme_Empty = "PagerScheme_Empty";

        internal const string PagerScheme_Classic = "PagerScheme_Classic";

        internal const string PagerScheme_Professional = "PagerScheme_Professional";

        internal const string PagerScheme_Colorful = "PagerScheme_Colorful";

        internal const string PagerScheme_Simple = "PagerScheme_Simple";

        internal const string PasswordRecoveryScheme_Empty = "PasswordRecoveryScheme_Empty";

        internal const string PasswordRecoveryScheme_Classic = "PasswordRecoveryScheme_Classic";

        internal const string PasswordRecoveryScheme_Elegant = "PasswordRecoveryScheme_Elegant";

        internal const string PasswordRecoveryScheme_Simple = "PasswordRecoveryScheme_Simple";

        internal const string PasswordRecoveryScheme_Professional = "PasswordRecoveryScheme_Professional";

        internal const string PasswordRecoveryScheme_Colorful = "PasswordRecoveryScheme_Colorful";

        internal const string PasswordRecovery_QuestionView = "PasswordRecovery_QuestionView";

        internal const string PasswordRecovery_SuccessView = "PasswordRecovery_SuccessView";

        internal const string PasswordRecovery_UserNameView = "PasswordRecovery_UserNameView";

        internal const string PasswordRecoveryAutoFormat_UserName = "PasswordRecoveryAutoFormat_UserName";

        internal const string PasswordRecoveryAutoFormat_HelpPageText = "PasswordRecoveryAutoFormat_HelpPageText";

        internal const string MailFilePicker_Caption = "MailFilePicker_Caption";

        internal const string MailFilePicker_Filter = "MailFilePicker_Filter";

        internal const string Xml_Inst = "Xml_Inst";

        internal const string MailDefinitionBodyFileNameEditor_DefaultCaption = "MailDefinitionBodyFileNameEditor_DefaultCaption";

        internal const string MailDefinitionBodyFileNameEditor_DefaultFilter = "MailDefinitionBodyFileNameEditor_DefaultFilter";

        internal const string UrlPicker_DefaultCaption = "UrlPicker_DefaultCaption";

        internal const string UrlPicker_DefaultFilter = "UrlPicker_DefaultFilter";

        internal const string UrlPicker_ImageCaption = "UrlPicker_ImageCaption";

        internal const string UrlPicker_ImageFilter = "UrlPicker_ImageFilter";

        internal const string UrlPicker_XmlCaption = "UrlPicker_XmlCaption";

        internal const string UrlPicker_XmlFilter = "UrlPicker_XmlFilter";

        internal const string UrlPicker_XslCaption = "UrlPicker_XslCaption";

        internal const string UrlPicker_XslFilter = "UrlPicker_XslFilter";

        internal const string XMLFilePicker_Caption = "XMLFilePicker_Caption";

        internal const string XMLFilePicker_Filter = "XMLFilePicker_Filter";

        internal const string DataBindingGlyph_ToolTip = "DataBindingGlyph_ToolTip";

        internal const string ExpressionBindingGlyph_ToolTip = "ExpressionBindingGlyph_ToolTip";

        internal const string ImplicitExpressionBindingGlyph_ToolTip = "ImplicitExpressionBindingGlyph_ToolTip";

        internal const string TemplateEdit_Tip = "TemplateEdit_Tip";

        internal const string RegexEditor_Title = "RegexEditor_Title";

        internal const string RegexEditor_StdExp = "RegexEditor_StdExp";

        internal const string RegexEditor_Validate = "RegexEditor_Validate";

        internal const string RegexEditor_SampleInput = "RegexEditor_SampleInput";

        internal const string RegexEditor_TestExpression = "RegexEditor_TestExpression";

        internal const string RegexEditor_ValidationExpression = "RegexEditor_ValidationExpression";

        internal const string RegexEditor_InputValid = "RegexEditor_InputValid";

        internal const string RegexEditor_InputInvalid = "RegexEditor_InputInvalid";

        internal const string RegexEditor_BadExpression = "RegexEditor_BadExpression";

        internal const string RegexEditor_Help = "RegexEditor_Help";

        internal const string RegexCanned_Custom = "RegexCanned_Custom";

        internal const string RegexCanned_Zip = "RegexCanned_Zip";

        internal const string RegexCanned_SocialSecurity = "RegexCanned_SocialSecurity";

        internal const string RegexCanned_USPhone = "RegexCanned_USPhone";

        internal const string RegexCanned_Email = "RegexCanned_Email";

        internal const string RegexCanned_URL = "RegexCanned_URL";

        internal const string RegexCanned_FrZip = "RegexCanned_FrZip";

        internal const string RegexCanned_FrPhone = "RegexCanned_FrPhone";

        internal const string RegexCanned_DeZip = "RegexCanned_DeZip";

        internal const string RegexCanned_DePhone = "RegexCanned_DePhone";

        internal const string RegexCanned_JpnZip = "RegexCanned_JpnZip";

        internal const string RegexCanned_JpnPhone = "RegexCanned_JpnPhone";

        internal const string RegexCanned_PrcZip = "RegexCanned_PrcZip";

        internal const string RegexCanned_PrcPhone = "RegexCanned_PrcPhone";

        internal const string RegexCanned_PrcSocialSecurity = "RegexCanned_PrcSocialSecurity";

        internal const string RegexCanned_Zip_Format = "RegexCanned_Zip_Format";

        internal const string RegexCanned_SocialSecurity_Format = "RegexCanned_SocialSecurity_Format";

        internal const string RegexCanned_USPhone_Format = "RegexCanned_USPhone_Format";

        internal const string RegexCanned_FrZip_Format = "RegexCanned_FrZip_Format";

        internal const string RegexCanned_FrPhone_Format = "RegexCanned_FrPhone_Format";

        internal const string RegexCanned_DeZip_Format = "RegexCanned_DeZip_Format";

        internal const string RegexCanned_DePhone_Format = "RegexCanned_DePhone_Format";

        internal const string RegexCanned_JpnZip_Format = "RegexCanned_JpnZip_Format";

        internal const string RegexCanned_JpnPhone_Format = "RegexCanned_JpnPhone_Format";

        internal const string RegexCanned_PrcZip_Format = "RegexCanned_PrcZip_Format";

        internal const string RegexCanned_PrcPhone_Format = "RegexCanned_PrcPhone_Format";

        internal const string RegexCanned_PrcSocialSecurity_Format = "RegexCanned_PrcSocialSecurity_Format";

        internal const string TemplateEditableDesignerRegion_CannotSetSupportsDataBinding = "TemplateEditableDesignerRegion_CannotSetSupportsDataBinding";

        internal const string TemplateDefinition_InvalidTemplateProperty = "TemplateDefinition_InvalidTemplateProperty";

        internal const string WrongType = "WrongType";

        internal const string Toolbox_OnWebformsPage = "Toolbox_OnWebformsPage";

        internal const string Toolbox_BadAttributeType = "Toolbox_BadAttributeType";

        internal const string TreeViewImageGenerator_ExpandImage = "TreeViewImageGenerator_ExpandImage";

        internal const string TreeViewImageGenerator_CollapseImage = "TreeViewImageGenerator_CollapseImage";

        internal const string TreeViewImageGenerator_NoExpandImage = "TreeViewImageGenerator_NoExpandImage";

        internal const string TreeViewImageGenerator_Preview = "TreeViewImageGenerator_Preview";

        internal const string TreeViewImageGenerator_Properties = "TreeViewImageGenerator_Properties";

        internal const string TreeViewImageGenerator_SampleRoot = "TreeViewImageGenerator_SampleRoot";

        internal const string TreeViewImageGenerator_SampleParent = "TreeViewImageGenerator_SampleParent";

        internal const string TreeViewImageGenerator_SampleLeaf = "TreeViewImageGenerator_SampleLeaf";

        internal const string TreeViewImageGenerator_FolderName = "TreeViewImageGenerator_FolderName";

        internal const string TreeViewImageGenerator_DefaultFolderName = "TreeViewImageGenerator_DefaultFolderName";

        internal const string TreeViewImageGenerator_Title = "TreeViewImageGenerator_Title";

        internal const string TreeViewImageGenerator_LineColor = "TreeViewImageGenerator_LineColor";

        internal const string TreeViewImageGenerator_LineStyle = "TreeViewImageGenerator_LineStyle";

        internal const string TreeViewImageGenerator_LineWidth = "TreeViewImageGenerator_LineWidth";

        internal const string TreeViewImageGenerator_LineImageHeight = "TreeViewImageGenerator_LineImageHeight";

        internal const string TreeViewImageGenerator_LineImageWidth = "TreeViewImageGenerator_LineImageWidth";

        internal const string TreeViewImageGenerator_LineImagesGenerated = "TreeViewImageGenerator_LineImagesGenerated";

        internal const string TreeViewImageGenerator_MissingFolderName = "TreeViewImageGenerator_MissingFolderName";

        internal const string TreeViewImageGenerator_NonExistentFolderName = "TreeViewImageGenerator_NonExistentFolderName";

        internal const string TreeViewImageGenerator_ProgressBarName = "TreeViewImageGenerator_ProgressBarName";

        internal const string TreeViewImageGenerator_ImagePickerFilter = "TreeViewImageGenerator_ImagePickerFilter";

        internal const string TreeViewImageGenerator_TransparentColor = "TreeViewImageGenerator_TransparentColor";

        internal const string TreeViewImageGenerator_ErrorCreatingFolder = "TreeViewImageGenerator_ErrorCreatingFolder";

        internal const string TreeViewImageGenerator_InvalidFolderName = "TreeViewImageGenerator_InvalidFolderName";

        internal const string TreeViewImageGenerator_DocumentExists = "TreeViewImageGenerator_DocumentExists";

        internal const string TreeViewImageGenerator_ErrorWriting = "TreeViewImageGenerator_ErrorWriting";

        internal const string TreeViewImageGenerator_InvalidValue = "TreeViewImageGenerator_InvalidValue";

        internal const string TreeViewImageGenerator_CouldNotOpenImage = "TreeViewImageGenerator_CouldNotOpenImage";

        internal const string TreeViewImageGenerator_Yes = "TreeViewImageGenerator_Yes";

        internal const string TreeViewImageGenerator_No = "TreeViewImageGenerator_No";

        internal const string TreeViewImageGenerator_YesToAll = "TreeViewImageGenerator_YesToAll";

        internal const string TreeViewImageGenerator_HelpText = "TreeViewImageGenerator_HelpText";

        internal const string TreeNodeCollectionEditor_AddRoot = "TreeNodeCollectionEditor_AddRoot";

        internal const string TreeNodeCollectionEditor_AddChild = "TreeNodeCollectionEditor_AddChild";

        internal const string TreeNodeCollectionEditor_Remove = "TreeNodeCollectionEditor_Remove";

        internal const string TreeNodeCollectionEditor_MoveDown = "TreeNodeCollectionEditor_MoveDown";

        internal const string TreeNodeCollectionEditor_MoveUp = "TreeNodeCollectionEditor_MoveUp";

        internal const string TreeNodeCollectionEditor_Indent = "TreeNodeCollectionEditor_Indent";

        internal const string TreeNodeCollectionEditor_Unindent = "TreeNodeCollectionEditor_Unindent";

        internal const string TreeNodeCollectionEditor_OK = "TreeNodeCollectionEditor_OK";

        internal const string TreeNodeCollectionEditor_Cancel = "TreeNodeCollectionEditor_Cancel";

        internal const string TreeNodeCollectionEditor_Nodes = "TreeNodeCollectionEditor_Nodes";

        internal const string TreeNodeCollectionEditor_Properties = "TreeNodeCollectionEditor_Properties";

        internal const string TreeNodeCollectionEditor_Title = "TreeNodeCollectionEditor_Title";

        internal const string TreeNodeCollectionEditor_NewNodeText = "TreeNodeCollectionEditor_NewNodeText";

        internal const string TreeViewBindingsEditor_Apply = "TreeViewBindingsEditor_Apply";

        internal const string TreeViewBindingsEditor_AddBinding = "TreeViewBindingsEditor_AddBinding";

        internal const string TreeViewBindingsEditor_AutoGenerateBindings = "TreeViewBindingsEditor_AutoGenerateBindings";

        internal const string TreeViewBindingsEditor_Bindings = "TreeViewBindingsEditor_Bindings";

        internal const string TreeViewBindingsEditor_BindingProperties = "TreeViewBindingsEditor_BindingProperties";

        internal const string TreeViewBindingsEditor_Cancel = "TreeViewBindingsEditor_Cancel";

        internal const string TreeViewBindingsEditor_EmptyBindingText = "TreeViewBindingsEditor_EmptyBindingText";

        internal const string TreeViewBindingsEditor_OK = "TreeViewBindingsEditor_OK";

        internal const string TreeViewBindingsEditor_Schema = "TreeViewBindingsEditor_Schema";

        internal const string TreeViewBindingsEditor_Title = "TreeViewBindingsEditor_Title";

        internal const string TreeViewDesigner_CreateLineImagesTransactionDescription = "TreeViewDesigner_CreateLineImagesTransactionDescription";

        internal const string TreeViewDesigner_DataActionGroup = "TreeViewDesigner_DataActionGroup";

        internal const string TreeViewDesigner_EditBindingsTransactionDescription = "TreeViewDesigner_EditBindingsTransactionDescription";

        internal const string TreeViewDesigner_EditNodesTransactionDescription = "TreeViewDesigner_EditNodesTransactionDescription";

        internal const string TreeViewDesigner_EditNodesDescription = "TreeViewDesigner_EditNodesDescription";

        internal const string TreeViewDesigner_EditBindings = "TreeViewDesigner_EditBindings";

        internal const string TreeViewDesigner_EditBindingsDescription = "TreeViewDesigner_EditBindingsDescription";

        internal const string TreeViewDesigner_EditNodes = "TreeViewDesigner_EditNodes";

        internal const string TreeViewDesigner_CreateLineImages = "TreeViewDesigner_CreateLineImages";

        internal const string TreeViewDesigner_CreateLineImagesDescription = "TreeViewDesigner_CreateLineImagesDescription";

        internal const string TreeViewDesigner_Empty = "TreeViewDesigner_Empty";

        internal const string TreeViewDesigner_EmptyDataBinding = "TreeViewDesigner_EmptyDataBinding";

        internal const string TreeViewDesigner_Error = "TreeViewDesigner_Error";

        internal const string TreeViewDesigner_ShowLines = "TreeViewDesigner_ShowLines";

        internal const string TreeViewDesigner_ShowLinesDescription = "TreeViewDesigner_ShowLinesDescription";

        internal const string TreeViewBindingsEditor_MoveBindingUpName = "TreeViewBindingsEditor_MoveBindingUpName";

        internal const string TreeViewBindingsEditor_MoveBindingUpDescription = "TreeViewBindingsEditor_MoveBindingUpDescription";

        internal const string TreeViewBindingsEditor_MoveBindingDownName = "TreeViewBindingsEditor_MoveBindingDownName";

        internal const string TreeViewBindingsEditor_MoveBindingDownDescription = "TreeViewBindingsEditor_MoveBindingDownDescription";

        internal const string TreeViewBindingsEditor_DeleteBindingName = "TreeViewBindingsEditor_DeleteBindingName";

        internal const string TreeViewBindingsEditor_DeleteBindingDescription = "TreeViewBindingsEditor_DeleteBindingDescription";

        internal const string TVScheme_Empty = "TVScheme_Empty";

        internal const string TVScheme_XP_File_Explorer = "TVScheme_XP_File_Explorer";

        internal const string TVScheme_MSDN = "TVScheme_MSDN";

        internal const string TVScheme_Windows_Help = "TVScheme_Windows_Help";

        internal const string TVScheme_Simple = "TVScheme_Simple";

        internal const string TVScheme_Simple2 = "TVScheme_Simple2";

        internal const string TVScheme_BulletedList = "TVScheme_BulletedList";

        internal const string TVScheme_BulletedList2 = "TVScheme_BulletedList2";

        internal const string TVScheme_BulletedList3 = "TVScheme_BulletedList3";

        internal const string TVScheme_BulletedList4 = "TVScheme_BulletedList4";

        internal const string TVScheme_BulletedList5 = "TVScheme_BulletedList5";

        internal const string TVScheme_BulletedList6 = "TVScheme_BulletedList6";

        internal const string TVScheme_Arrows = "TVScheme_Arrows";

        internal const string TVScheme_Arrows2 = "TVScheme_Arrows2";

        internal const string TVScheme_TOC = "TVScheme_TOC";

        internal const string TVScheme_News = "TVScheme_News";

        internal const string TVScheme_Contacts = "TVScheme_Contacts";

        internal const string TVScheme_Inbox = "TVScheme_Inbox";

        internal const string TVScheme_Events = "TVScheme_Events";

        internal const string TVScheme_FAQ = "TVScheme_FAQ";

        internal const string UserControlDesigner_MissingID = "UserControlDesigner_MissingID";

        internal const string UserControlDesigner_EditUserControl = "UserControlDesigner_EditUserControl";

        internal const string UserControlDesigner_Refresh = "UserControlDesigner_Refresh";

        internal const string UserControlDesigner_NotFound = "UserControlDesigner_NotFound";

        internal const string UserControlDesigner_CyclicError = "UserControlDesigner_CyclicError";

        internal const string WebPartScheme_Empty = "WebPartScheme_Empty";

        internal const string WebPartScheme_Professional = "WebPartScheme_Professional";

        internal const string WebPartScheme_Simple = "WebPartScheme_Simple";

        internal const string WebPartScheme_Classic = "WebPartScheme_Classic";

        internal const string WebPartScheme_Colorful = "WebPartScheme_Colorful";

        internal const string CatalogZoneDesigner_OnlyCatalogParts = "CatalogZoneDesigner_OnlyCatalogParts";

        internal const string CatalogZoneDesigner_Empty = "CatalogZoneDesigner_Empty";

        internal const string DesignerCatalogPartChrome_TypeCatalogPart = "DesignerCatalogPartChrome_TypeCatalogPart";

        internal const string DesignerEditorPartChrome_TypeEditorPart = "DesignerEditorPartChrome_TypeEditorPart";

        internal const string EditorZoneDesigner_OnlyEditorParts = "EditorZoneDesigner_OnlyEditorParts";

        internal const string EditorZoneDesigner_Empty = "EditorZoneDesigner_Empty";

        internal const string DeclarativeCatalogPartDesigner_Empty = "DeclarativeCatalogPartDesigner_Empty";

        internal const string ToolZoneDesigner_ViewInBrowseMode = "ToolZoneDesigner_ViewInBrowseMode";

        internal const string ToolZoneDesigner_ViewInBrowseModeDesc = "ToolZoneDesigner_ViewInBrowseModeDesc";

        internal const string WebPartZoneAutoFormat_SampleWebPartTitle = "WebPartZoneAutoFormat_SampleWebPartTitle";

        internal const string WebPartZoneAutoFormat_SampleWebPartContents = "WebPartZoneAutoFormat_SampleWebPartContents";

        internal const string CatalogZone_SampleWebPartTitle = "CatalogZone_SampleWebPartTitle";

        internal const string WebPartZoneDesigner_Empty = "WebPartZoneDesigner_Empty";

        internal const string WebPartZoneDesigner_Error = "WebPartZoneDesigner_Error";

        internal const string RTL = "RTL";

        internal const string Sample_Column = "Sample_Column";

        internal const string Sample_Databound_Column = "Sample_Databound_Column";

        internal const string Sample_Databound_Text = "Sample_Databound_Text";

        internal const string Sample_Databound_Text_Alt = "Sample_Databound_Text_Alt";

        internal const string Sample_Unbound_Text = "Sample_Unbound_Text";

        internal const string DesignTimeData_BadDataMember = "DesignTimeData_BadDataMember";

        internal const string TrayLineUpIcons = "TrayLineUpIcons";

        internal const string TrayAutoArrange = "TrayAutoArrange";

        internal const string TrayShowLargeIcons = "TrayShowLargeIcons";

        internal const string StringDictionaryEditorTitle = "StringDictionaryEditorTitle";

        internal const string StartFileNameEditorTitle = "StartFileNameEditorTitle";

        internal const string StartFileNameEditorAllFiles = "StartFileNameEditorAllFiles";

        internal const string ToolStripItemCollectionEditorVerb = "ToolStripItemCollectionEditorVerb";

        internal const string ToolStripDropDownItemCollectionEditorVerb = "ToolStripDropDownItemCollectionEditorVerb";

        internal const string ToolStripItemCollectionEditorLabelNone = "ToolStripItemCollectionEditorLabelNone";

        internal const string ToolStripItemCollectionEditorLabelMultipleItems = "ToolStripItemCollectionEditorLabelMultipleItems";

        internal const string ContextMenuViewCode = "ContextMenuViewCode";

        internal const string ContextMenuDocumentOutline = "ContextMenuDocumentOutline";

        internal const string ContextMenuBringToFront = "ContextMenuBringToFront";

        internal const string ContextMenuSendToBack = "ContextMenuSendToBack";

        internal const string ContextMenuAlignToGrid = "ContextMenuAlignToGrid";

        internal const string ContextMenuLockControls = "ContextMenuLockControls";

        internal const string ContextMenuSelect = "ContextMenuSelect";

        internal const string ContextMenuCut = "ContextMenuCut";

        internal const string ContextMenuCopy = "ContextMenuCopy";

        internal const string ContextMenuPaste = "ContextMenuPaste";

        internal const string ContextMenuDelete = "ContextMenuDelete";

        internal const string ContextMenuProperties = "ContextMenuProperties";

        internal const string ToolStripItemContextMenuSetImage = "ToolStripItemContextMenuSetImage";

        internal const string ToolStripItemContextMenuConvertTo = "ToolStripItemContextMenuConvertTo";

        internal const string ToolStripItemContextMenuInsert = "ToolStripItemContextMenuInsert";

        internal const string ToolStripActionList_Name = "ToolStripActionList_Name";

        internal const string ToolStripActionList_NameDesc = "ToolStripActionList_NameDesc";

        internal const string ToolStripActionList_Behavior = "ToolStripActionList_Behavior";

        internal const string ToolStripActionList_Visible = "ToolStripActionList_Visible";

        internal const string ToolStripActionList_VisibleDesc = "ToolStripActionList_VisibleDesc";

        internal const string ToolStripActionList_ShowItemToolTips = "ToolStripActionList_ShowItemToolTips";

        internal const string ToolStripActionList_ShowItemToolTipsDesc = "ToolStripActionList_ShowItemToolTipsDesc";

        internal const string ToolStripActionList_AllowItemReorder = "ToolStripActionList_AllowItemReorder";

        internal const string ToolStripActionList_AllowItemReorderDesc = "ToolStripActionList_AllowItemReorderDesc";

        internal const string ToolStripActionList_CanOverflow = "ToolStripActionList_CanOverflow";

        internal const string ToolStripActionList_CanOverflowDesc = "ToolStripActionList_CanOverflowDesc";

        internal const string ToolStripActionList_Layout = "ToolStripActionList_Layout";

        internal const string ToolStripActionList_Dock = "ToolStripActionList_Dock";

        internal const string ToolStripActionList_DockDesc = "ToolStripActionList_DockDesc";

        internal const string ToolStripActionList_Raft = "ToolStripActionList_Raft";

        internal const string ToolStripActionList_RaftDesc = "ToolStripActionList_RaftDesc";

        internal const string ToolStripActionList_RenderMode = "ToolStripActionList_RenderMode";

        internal const string ToolStripActionList_RenderModeDesc = "ToolStripActionList_RenderModeDesc";

        internal const string ToolStripActionList_GripStyle = "ToolStripActionList_GripStyle";

        internal const string ToolStripActionList_GripStyleDesc = "ToolStripActionList_GripStyleDesc";

        internal const string ToolStripActionList_Stretch = "ToolStripActionList_Stretch";

        internal const string ToolStripActionList_StretchDesc = "ToolStripActionList_StretchDesc";

        internal const string ToolStripActionList_SizingGrip = "ToolStripActionList_SizingGrip";

        internal const string ToolStripActionList_SizingGripDesc = "ToolStripActionList_SizingGripDesc";

        internal const string ToolStripContainerActionList_Show = "ToolStripContainerActionList_Show";

        internal const string ToolStripContainerActionList_Visible = "ToolStripContainerActionList_Visible";

        internal const string ToolStripContainerActionList_Top = "ToolStripContainerActionList_Top";

        internal const string ToolStripContainerActionList_TopDesc = "ToolStripContainerActionList_TopDesc";

        internal const string ToolStripContainerActionList_Bottom = "ToolStripContainerActionList_Bottom";

        internal const string ToolStripContainerActionList_BottomDesc = "ToolStripContainerActionList_BottomDesc";

        internal const string ToolStripContainerActionList_Left = "ToolStripContainerActionList_Left";

        internal const string ToolStripContainerActionList_LeftDesc = "ToolStripContainerActionList_LeftDesc";

        internal const string ToolStripContainerActionList_Right = "ToolStripContainerActionList_Right";

        internal const string ToolStripContainerActionList_RightDesc = "ToolStripContainerActionList_RightDesc";

        internal const string ContextMenuStripActionList_ShowImageMargin = "ContextMenuStripActionList_ShowImageMargin";

        internal const string ContextMenuStripActionList_ShowImageMarginDesc = "ContextMenuStripActionList_ShowImageMarginDesc";

        internal const string ContextMenuStripActionList_ShowCheckMargin = "ContextMenuStripActionList_ShowCheckMargin";

        internal const string ContextMenuStripActionList_ShowCheckMarginDesc = "ContextMenuStripActionList_ShowCheckMarginDesc";

        internal const string ContextMenuStripActionList_ShowShortCuts = "ContextMenuStripActionList_ShowShortCuts";

        internal const string ContextMenuStripActionList_ShowShortCutsDesc = "ContextMenuStripActionList_ShowShortCutsDesc";

        internal const string ToolStripDesignerTransactionAddingItem = "ToolStripDesignerTransactionAddingItem";

        internal const string ToolStripDesignerTransactionRemovingItem = "ToolStripDesignerTransactionRemovingItem";

        internal const string ToolStripDesignerSelectToolStripTransaction = "ToolStripDesignerSelectToolStripTransaction";

        internal const string ToolStripDesignerStandardItemsVerb = "ToolStripDesignerStandardItemsVerb";

        internal const string ToolStripDesignerEmbedVerb = "ToolStripDesignerEmbedVerb";

        internal const string ToolStripDesignerStandardItemsVerbDesc = "ToolStripDesignerStandardItemsVerbDesc";

        internal const string ToolStripDesignerEmbedVerbDesc = "ToolStripDesignerEmbedVerbDesc";

        internal const string ToolStripDesignerInsertItemsVerb = "ToolStripDesignerInsertItemsVerb";

        internal const string ToolStripAddingItem = "ToolStripAddingItem";

        internal const string ToolStripDesignerSelectAllVerb = "ToolStripDesignerSelectAllVerb";

        internal const string ToolStripSeparatorError = "ToolStripSeparatorError";

        internal const string ToolStripCircularReferenceError = "ToolStripCircularReferenceError";

        internal const string ToolStripDesignerTemplateNodeEnterText = "ToolStripDesignerTemplateNodeEnterText";

        internal const string ToolStripDesignerTemplateNodeSplitButtonToolTip = "ToolStripDesignerTemplateNodeSplitButtonToolTip";

        internal const string ToolStripDesignerTemplateNodeLabelToolTip = "ToolStripDesignerTemplateNodeLabelToolTip";

        internal const string ToolStripDesignerTemplateNodeSplitButtonStatusStripToolTip = "ToolStripDesignerTemplateNodeSplitButtonStatusStripToolTip";

        internal const string ToolStripDesignerFailedToLoadItemType = "ToolStripDesignerFailedToLoadItemType";

        internal const string ToolStripDesignerToolStripItemsOnly = "ToolStripDesignerToolStripItemsOnly";

        internal const string StandardMenuTitle = "StandardMenuTitle";

        internal const string StandardMenuStripTitle = "StandardMenuStripTitle";

        internal const string StandardMenuFile = "StandardMenuFile";

        internal const string StandardMenuNew = "StandardMenuNew";

        internal const string StandardMenuOpen = "StandardMenuOpen";

        internal const string StandardMenuSave = "StandardMenuSave";

        internal const string StandardMenuSaveAs = "StandardMenuSaveAs";

        internal const string StandardMenuPrint = "StandardMenuPrint";

        internal const string StandardMenuPrintPreview = "StandardMenuPrintPreview";

        internal const string StandardMenuExit = "StandardMenuExit";

        internal const string StandardMenuEdit = "StandardMenuEdit";

        internal const string StandardMenuUndo = "StandardMenuUndo";

        internal const string StandardMenuRedo = "StandardMenuRedo";

        internal const string StandardMenuCut = "StandardMenuCut";

        internal const string StandardToolCut = "StandardToolCut";

        internal const string StandardMenuCopy = "StandardMenuCopy";

        internal const string StandardMenuPaste = "StandardMenuPaste";

        internal const string StandardMenuDelete = "StandardMenuDelete";

        internal const string StandardMenuSelectAll = "StandardMenuSelectAll";

        internal const string StandardMenuTools = "StandardMenuTools";

        internal const string StandardMenuCustomize = "StandardMenuCustomize";

        internal const string StandardMenuOptions = "StandardMenuOptions";

        internal const string StandardMenuHelp = "StandardMenuHelp";

        internal const string StandardToolHelp = "StandardToolHelp";

        internal const string StandardMenuContents = "StandardMenuContents";

        internal const string StandardMenuIndex = "StandardMenuIndex";

        internal const string StandardMenuSearch = "StandardMenuSearch";

        internal const string StandardMenuAbout = "StandardMenuAbout";

        internal const string StandardMenuCreateDesc = "StandardMenuCreateDesc";

        internal const string CG_DataSetGeneratorFail_InputFileEmpty = "CG_DataSetGeneratorFail_InputFileEmpty";

        internal const string CG_DataSetGeneratorFail_DatasetNull = "CG_DataSetGeneratorFail_DatasetNull";

        internal const string CG_DataSetGeneratorFail_CodeGeneratorNull = "CG_DataSetGeneratorFail_CodeGeneratorNull";

        internal const string CG_DataSetGeneratorFail_CodeNamespaceNull = "CG_DataSetGeneratorFail_CodeNamespaceNull";

        internal const string CG_DataSetGeneratorFail_UnableToConvertToDataSet = "CG_DataSetGeneratorFail_UnableToConvertToDataSet";

        internal const string CG_DataSetGeneratorFail_FailToGenerateCode = "CG_DataSetGeneratorFail_FailToGenerateCode";

        internal const string CG_TypeCantBeNull = "CG_TypeCantBeNull";

        internal const string CG_NoCtor0 = "CG_NoCtor0";

        internal const string CG_NoCtor1 = "CG_NoCtor1";

        internal const string CG_MainSelectCommandNotSet = "CG_MainSelectCommandNotSet";

        internal const string CG_UnableToReadExtProperties = "CG_UnableToReadExtProperties";

        internal const string CG_UnableToConvertSqlDbTypeToSqlType = "CG_UnableToConvertSqlDbTypeToSqlType";

        internal const string CG_UnableToConvertDbTypeToUrtType = "CG_UnableToConvertDbTypeToUrtType";

        internal const string CG_RowColumnPropertyNameFixup = "CG_RowColumnPropertyNameFixup";

        internal const string CG_DataSourceClassNameFixup = "CG_DataSourceClassNameFixup";

        internal const string CG_TablePropertyNameFixup = "CG_TablePropertyNameFixup";

        internal const string CG_TableSourceNameFixup = "CG_TableSourceNameFixup";

        internal const string CG_EmptyDSName = "CG_EmptyDSName";

        internal const string CG_ColumnIsDBNull = "CG_ColumnIsDBNull";

        internal const string CG_ParameterIsDBNull = "CG_ParameterIsDBNull";

        internal const string CG_TableAdapterManagerNeedsSameConnString = "CG_TableAdapterManagerNeedsSameConnString";

        internal const string CG_TableAdapterManagerHasNoConnection = "CG_TableAdapterManagerHasNoConnection";

        internal const string CG_TableAdapterManagerNotSupportTransaction = "CG_TableAdapterManagerNotSupportTransaction";

        internal const string DTDS_CouldNotDeserializeConnection = "DTDS_CouldNotDeserializeConnection";

        internal const string DTDS_CouldNotDeserializeXmlElement = "DTDS_CouldNotDeserializeXmlElement";

        internal const string DTDS_NameIsRequired = "DTDS_NameIsRequired";

        internal const string DTDS_NameConflict = "DTDS_NameConflict";

        internal const string DTDS_TableNotMatch = "DTDS_TableNotMatch";

        internal const string DD_E_TableDirectValidForOleDbOnly = "DD_E_TableDirectValidForOleDbOnly";

        internal const string CM_NameNotEmptyExcption = "CM_NameNotEmptyExcption";

        internal const string CM_NameTooLongExcption = "CM_NameTooLongExcption";

        internal const string CM_NameInvalid = "CM_NameInvalid";

        internal const string CM_NameExist = "CM_NameExist";

        internal const string PropertiesCategoryName = "PropertiesCategoryName";

        internal const string LinksCategoryName = "LinksCategoryName";

        internal const string ItemsCategoryName = "ItemsCategoryName";

        internal const string DataCategoryName = "DataCategoryName";

        internal const string ImageListActionListImageSizeDisplayName = "ImageListActionListImageSizeDisplayName";

        internal const string ImageListActionListImageSizeDescription = "ImageListActionListImageSizeDescription";

        internal const string ImageListActionListColorDepthDisplayName = "ImageListActionListColorDepthDisplayName";

        internal const string ImageListActionListColorDepthDescription = "ImageListActionListColorDepthDescription";

        internal const string ImageListActionListChooseImagesDisplayName = "ImageListActionListChooseImagesDisplayName";

        internal const string ImageListActionListChooseImagesDescription = "ImageListActionListChooseImagesDescription";

        internal const string ListControlUnboundActionListEditItemsDisplayName = "ListControlUnboundActionListEditItemsDisplayName";

        internal const string ListControlUnboundActionListEditItemsDescription = "ListControlUnboundActionListEditItemsDescription";

        internal const string ListViewActionListEditItemsDisplayName = "ListViewActionListEditItemsDisplayName";

        internal const string ListViewActionListEditItemsDescription = "ListViewActionListEditItemsDescription";

        internal const string ListViewActionListEditColumnsDisplayName = "ListViewActionListEditColumnsDisplayName";

        internal const string ListViewActionListEditColumnsDescription = "ListViewActionListEditColumnsDescription";

        internal const string ListViewActionListEditGroupsDisplayName = "ListViewActionListEditGroupsDisplayName";

        internal const string ListViewActionListEditGroupsDescription = "ListViewActionListEditGroupsDescription";

        internal const string ListViewActionListViewDisplayName = "ListViewActionListViewDisplayName";

        internal const string ListViewActionListViewDescription = "ListViewActionListViewDescription";

        internal const string ListViewActionListSmallImagesDisplayName = "ListViewActionListSmallImagesDisplayName";

        internal const string ListViewActionListSmallImagesDescription = "ListViewActionListSmallImagesDescription";

        internal const string ListViewActionListLargeImagesDisplayName = "ListViewActionListLargeImagesDisplayName";

        internal const string ListViewActionListLargeImagesDescription = "ListViewActionListLargeImagesDescription";

        internal const string BoundModeHeader = "BoundModeHeader";

        internal const string UnBoundModeHeader = "UnBoundModeHeader";

        internal const string BoundModeDisplayName = "BoundModeDisplayName";

        internal const string BoundModeDescription = "BoundModeDescription";

        internal const string DataSourceDisplayName = "DataSourceDisplayName";

        internal const string DataSourceDescription = "DataSourceDescription";

        internal const string DisplayMemberDisplayName = "DisplayMemberDisplayName";

        internal const string DisplayMemberDescription = "DisplayMemberDescription";

        internal const string ValueMemberDisplayName = "ValueMemberDisplayName";

        internal const string ValueMemberDescription = "ValueMemberDescription";

        internal const string BoundSelectedValueDisplayName = "BoundSelectedValueDisplayName";

        internal const string BoundSelectedValueDescription = "BoundSelectedValueDescription";

        internal const string EditItemDisplayName = "EditItemDisplayName";

        internal const string EditItemDescription = "EditItemDescription";

        internal const string ChooseImageDisplayName = "ChooseImageDisplayName";

        internal const string ChooseImageDescription = "ChooseImageDescription";

        internal const string SizeModeDisplayName = "SizeModeDisplayName";

        internal const string SizeModeDescription = "SizeModeDescription";

        internal const string EditLinesDisplayName = "EditLinesDisplayName";

        internal const string EditLinesDescription = "EditLinesDescription";

        internal const string MultiLineDisplayName = "MultiLineDisplayName";

        internal const string MultiLineDescription = "MultiLineDescription";

        internal const string ChooseIconDisplayName = "ChooseIconDisplayName";

        internal const string InvokeNodesDialogDisplayName = "InvokeNodesDialogDisplayName";

        internal const string InvokeNodesDialogDescription = "InvokeNodesDialogDescription";

        internal const string ImageListDisplayName = "ImageListDisplayName";

        internal const string ImageListDescription = "ImageListDescription";

        internal const string DesignerOptions_LayoutSettings = "DesignerOptions_LayoutSettings";

        internal const string DesignerOptions_ObjectBoundSmartTagSettings = "DesignerOptions_ObjectBoundSmartTagSettings";

        internal const string DesignerOptions_GridSizeDesc = "DesignerOptions_GridSizeDesc";

        internal const string DesignerOptions_GridSizeDisplayName = "DesignerOptions_GridSizeDisplayName";

        internal const string DesignerOptions_ShowGridDesc = "DesignerOptions_ShowGridDesc";

        internal const string DesignerOptions_ShowGridDisplayName = "DesignerOptions_ShowGridDisplayName";

        internal const string DesignerOptions_SnapToGridDesc = "DesignerOptions_SnapToGridDesc";

        internal const string DesignerOptions_SnapToGridDisplayName = "DesignerOptions_SnapToGridDisplayName";

        internal const string DesignerOptions_UseSnapLines = "DesignerOptions_UseSnapLines";

        internal const string DesignerOptions_UseSmartTags = "DesignerOptions_UseSmartTags";

        internal const string DesignerOptions_ObjectBoundSmartTagAutoShow = "DesignerOptions_ObjectBoundSmartTagAutoShow";

        internal const string DesignerOptions_ObjectBoundSmartTagAutoShowDisplayName = "DesignerOptions_ObjectBoundSmartTagAutoShowDisplayName";

        internal const string DesignerOptions_CodeGenSettings = "DesignerOptions_CodeGenSettings";

        internal const string DesignerOptions_OptimizedCodeGen = "DesignerOptions_OptimizedCodeGen";

        internal const string DesignerOptions_CodeGenDisplay = "DesignerOptions_CodeGenDisplay";

        internal const string DesignerOptions_EnableInSituEditingDisplay = "DesignerOptions_EnableInSituEditingDisplay";

        internal const string DesignerOptions_EnableInSituEditingCat = "DesignerOptions_EnableInSituEditingCat";

        internal const string DesignerOptions_EnableInSituEditingDesc = "DesignerOptions_EnableInSituEditingDesc";

        internal const string ClassComments1 = "ClassComments1";

        internal const string ClassComments2 = "ClassComments2";

        internal const string ClassComments3 = "ClassComments3";

        internal const string ClassComments4 = "ClassComments4";

        internal const string ClassDocComment = "ClassDocComment";

        internal const string StringPropertyComment = "StringPropertyComment";

        internal const string StringPropertyTruncatedComment = "StringPropertyTruncatedComment";

        internal const string NonStringPropertyComment = "NonStringPropertyComment";

        internal const string NonStringPropertyDetailedComment = "NonStringPropertyDetailedComment";

        internal const string CulturePropertyComment1 = "CulturePropertyComment1";

        internal const string CulturePropertyComment2 = "CulturePropertyComment2";

        internal const string ResMgrPropertyComment = "ResMgrPropertyComment";

        internal const string MismatchedResourceName = "MismatchedResourceName";

        internal const string InvalidIdentifier = "InvalidIdentifier";

        internal const string DirectiveRegistry_UnknownFramework = "DirectiveRegistry_UnknownFramework"; 
        #endregion
    }
}

