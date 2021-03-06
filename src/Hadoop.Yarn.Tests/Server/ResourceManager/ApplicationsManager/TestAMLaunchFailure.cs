using Sharpen;

namespace Org.Apache.Hadoop.Yarn.Server.Resourcemanager.Applicationsmanager
{
	public class TestAMLaunchFailure
	{
		/* a test case that tests the launch failure of a AM */
		//  private static final Log LOG = LogFactory.getLog(TestAMLaunchFailure.class);
		//  private static final RecordFactory recordFactory = RecordFactoryProvider.getRecordFactory(null);
		//  ApplicationsManagerImpl asmImpl;
		//  YarnScheduler scheduler = new DummyYarnScheduler();
		//  ApplicationTokenSecretManager applicationTokenSecretManager =
		//    new ApplicationTokenSecretManager();
		//  private ClientRMService clientService;
		//
		//  private RMContext context;
		//
		//  private static class DummyYarnScheduler implements YarnScheduler {
		//    private Container container = recordFactory.newRecordInstance(Container.class);
		//
		//    @Override
		//    public Allocation allocate(ApplicationId applicationId,
		//        List<ResourceRequest> ask, List<Container> release) throws IOException {
		//      return new Allocation(Arrays.asList(container), Resources.none());
		//    }
		//
		//    @Override
		//    public QueueInfo getQueueInfo(String queueName,
		//        boolean includeChildQueues,
		//        boolean recursive) throws IOException {
		//      return null;
		//    }
		//
		//    @Override
		//    public List<QueueUserACLInfo> getQueueUserAclInfo() {
		//      return null;
		//    }
		//
		//    @Override
		//    public void addApplication(ApplicationId applicationId,
		//        ApplicationMaster master, String user, String queue, Priority priority
		//        , ApplicationStore appStore)
		//        throws IOException {
		//      // TODO Auto-generated method stub
		//
		//    }
		//
		//    @Override
		//    public Resource getMaximumResourceCapability() {
		//      // TODO Auto-generated method stub
		//      return null;
		//    }
		//
		//    @Override
		//    public Resource getMinimumResourceCapability() {
		//      // TODO Auto-generated method stub
		//      return null;
		//    }
		//  }
		//
		//  private class DummyApplicationTracker implements EventHandler<ASMEvent<ApplicationTrackerEventType>> {
		//    public DummyApplicationTracker() {
		//      context.getDispatcher().register(ApplicationTrackerEventType.class, this);
		//    }
		//    @Override
		//    public void handle(ASMEvent<ApplicationTrackerEventType> event) {
		//    }
		//  }
		//
		//  public class ExtApplicationsManagerImpl extends ApplicationsManagerImpl {
		//
		//    private  class DummyApplicationMasterLauncher implements EventHandler<ASMEvent<AMLauncherEventType>> {
		//      private AtomicInteger notify = new AtomicInteger();
		//      private AppAttempt app;
		//
		//      public DummyApplicationMasterLauncher(RMContext context) {
		//        context.getDispatcher().register(AMLauncherEventType.class, this);
		//        new TestThread().start();
		//      }
		//      @Override
		//      public void handle(ASMEvent<AMLauncherEventType> appEvent) {
		//        switch(appEvent.getType()) {
		//        case LAUNCH:
		//          LOG.info("LAUNCH called ");
		//          app = appEvent.getApplication();
		//          synchronized (notify) {
		//            notify.addAndGet(1);
		//            notify.notify();
		//          }
		//          break;
		//        }
		//      }
		//
		//      private class TestThread extends Thread {
		//        public void run() {
		//          synchronized(notify) {
		//            try {
		//              while (notify.get() == 0) {
		//                notify.wait();
		//              }
		//            } catch (InterruptedException e) {
		//              e.printStackTrace();
		//            }
		//            context.getDispatcher().getEventHandler().handle(
		//                new ApplicationEvent(ApplicationEventType.LAUNCHED,
		//                    app.getApplicationID()));
		//          }
		//        }
		//      }
		//    }
		//
		//    public ExtApplicationsManagerImpl(
		//        ApplicationTokenSecretManager applicationTokenSecretManager,
		//        YarnScheduler scheduler) {
		//      super(applicationTokenSecretManager, scheduler, context);
		//    }
		//
		//    @Override
		//    protected EventHandler<ASMEvent<AMLauncherEventType>> createNewApplicationMasterLauncher(
		//        ApplicationTokenSecretManager tokenSecretManager) {
		//      return new DummyApplicationMasterLauncher(context);
		//    }
		//  }
		//
		//
		//  @Before
		//  public void setUp() {
		//    context = new RMContextImpl(new MemStore());
		//    Configuration conf = new Configuration();
		//
		//    context.getDispatcher().register(ApplicationEventType.class,
		//        new ResourceManager.ApplicationEventDispatcher(context));
		//
		//    context.getDispatcher().init(conf);
		//    context.getDispatcher().start();
		//
		//    asmImpl = new ExtApplicationsManagerImpl(applicationTokenSecretManager, scheduler);
		//    clientService = new ClientRMService(context, asmImpl
		//        .getAmLivelinessMonitor(), asmImpl.getClientToAMSecretManager(),
		//        scheduler);
		//    clientService.init(conf);
		//    new DummyApplicationTracker();
		//    conf.setLong(YarnConfiguration.AM_EXPIRY_INTERVAL, 3000L);
		//    conf.setInt(RMConfig.AM_MAX_RETRIES, 1);
		//    asmImpl.init(conf);
		//    asmImpl.start();
		//  }
		//
		//  @After
		//  public void tearDown() {
		//    asmImpl.stop();
		//  }
		//
		//  private ApplicationSubmissionContext createDummyAppContext(ApplicationId appID) {
		//    ApplicationSubmissionContext context = recordFactory.newRecordInstance(ApplicationSubmissionContext.class);
		//    context.setApplicationId(appID);
		//    return context;
		//  }
		//
		//  @Test
		//  public void testAMLaunchFailure() throws Exception {
		//    ApplicationId appID = clientService.getNewApplicationId();
		//    ApplicationSubmissionContext submissionContext = createDummyAppContext(appID);
		//    SubmitApplicationRequest request = recordFactory
		//        .newRecordInstance(SubmitApplicationRequest.class);
		//    request.setApplicationSubmissionContext(submissionContext);
		//    clientService.submitApplication(request);
		//    AppAttempt application = context.getApplications().get(appID);
		//
		//    while (application.getState() != ApplicationState.FAILED) {
		//      LOG.info("Waiting for application to go to FAILED state."
		//          + " Current state is " + application.getState());
		//      Thread.sleep(200);
		//      application = context.getApplications().get(appID);
		//    }
		//    Assert.assertEquals(ApplicationState.FAILED, application.getState());
		//  }
	}
}
